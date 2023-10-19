using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Finderr.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Finderr.Models.ViewModels;
using Finderr.Data;

namespace Finderr.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly ApplicationDbContext _context;

        public UserProfileController(UserManager<UserProfile> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: UserProfile/Edit
        public async Task<IActionResult> Edit()
        {
            var id = _userManager.GetUserId(User);
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userView = new UserProfileView
            {
                Id = user.Id,
                Name = user.Name,
                Occupation = user.Occupation,
                CurrentAddress = user.CurrentAddress,
                ProfilePrivacy = user.ProfilePrivacy,
                ProfilePictureReference = user.ProfilePictureReference,
                FacebookLink = user.FacebookLink,
                TwitterLink = user.TwitterLink,
                LinkedInLink = user.LinkedInLink
            };
            return View(userView);
        }

        // POST: UserProfile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Occupation,CurrentAddress,ProfilePrivacy,ProfilePictureReference,FacebookLink,TwitterLink,LinkedInLink,ProfileImage")] UserProfileView userProfileView, IFormFile ProfileImage)
        {
            if (id != userProfileView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the user profile in the database
                    var user = await _context.UserProfile.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    var profileImagePath = "Images/";
                    var uniqueFileName = Guid.NewGuid() + "_" + ProfileImage.FileName;
                    var imagePath = Path.Combine(profileImagePath, uniqueFileName);
                    var imageUrl = Path.Combine("/", imagePath);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(stream);
                    }
                    user.ProfilePictureReference = imageUrl;
                    user.Name = userProfileView.Name;
                    user.Occupation = userProfileView.Occupation;
                    user.CurrentAddress = userProfileView.CurrentAddress;
                    user.ProfilePrivacy = userProfileView.ProfilePrivacy;
                    user.FacebookLink = userProfileView.FacebookLink;
                    user.TwitterLink = userProfileView.TwitterLink;
                    user.LinkedInLink = userProfileView.LinkedInLink;

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details"); // Redirect to the user's profile page
            }
            return View(userProfileView);
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var id = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Check if the user profile exists
        private bool UserProfileExists(string id)
        {
            return _userManager.FindByIdAsync(id).Result != null;
        }
    }
}
