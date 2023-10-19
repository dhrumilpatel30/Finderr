using Finderr.Data;
using Finderr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finderr.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: SearchController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SearchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(IFormCollection collection)
        {
            String? GroupName = collection["GroupName"];
            String? UserName = collection["UserName"];
            String? Location = collection["Location"];
            String? Occupation = collection["Occupation"];

            if ("" != UserName)
            {
                UserProfile? user = _context.UserProfile.Where(u => u.Name.Contains(UserName)).FirstOrDefault();
                if(null != user)
                {
                    return View(new List<UserProfileTemp>());
                }
                List<UserProfileTemp> temp = new()
                {
                    new UserProfileTemp
                    {
                        Name = user!.Name,
                        CurrentAddress = user.CurrentAddress,
                        Occupation = user.Occupation,
                        LinkedInLink = user.LinkedInLink,
                        TwitterLink = user.TwitterLink,
                        FacebookLink = user.FacebookLink,
                        ProfilePrivacy = user.ProfilePrivacy,
                        ProfilePictureReference = user.ProfilePictureReference,
                    }
                };
                return View(temp);
            }
            List<UserProfile> users;
            if ("" != GroupName)
            {
                users = _context.GroupMember.Where(gm => gm.Group!.GroupName.Contains(GroupName)).Select(gm => gm.UserProfile!).ToList();
            }
            else
            {
                users = _context.UserProfile.ToList();
            }
            if("" != Location)
            {
                users = users.Where(u => u.CurrentAddress.Contains(Location)).ToList();
            }
            if("" != Occupation)
            {
                users = users.Where(u => u.Occupation.Contains(Occupation)).ToList();
            }

            List<UserProfileTemp> userProfiles = new();

            foreach (UserProfile user in users)
            {
                userProfiles.Add(new UserProfileTemp
                {
                    Name = user.Name,
                    CurrentAddress = user.CurrentAddress,
                    Occupation = user.Occupation,
                    LinkedInLink = user.LinkedInLink,
                    TwitterLink = user.TwitterLink,
                    FacebookLink = user.FacebookLink,
                    ProfilePrivacy = user.ProfilePrivacy,
                    ProfilePictureReference = user.ProfilePictureReference,
                });
            }

            return View(userProfiles);

        }

        // GET: SearchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
