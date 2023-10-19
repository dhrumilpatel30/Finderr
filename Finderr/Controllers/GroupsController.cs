using Finderr.Data;
using Finderr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Finderr.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            List<Group> groups = new();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var userProfile = await _context.UserProfile.Where(u => u.Id == userId).FirstAsync();
                var groupMembers = await _context.GroupMember.Where(gm => gm.UserProfileId == userProfile.Id).ToListAsync();
                foreach (var groupMember in groupMembers)
                {
                    var groupId = groupMember.GroupId;
                    var group = await _context.Group.Where(g => g.GroupId == groupId).FirstAsync();
                    if ("true" == groupMember.IsAdmin)
                    {
                        groups.Add(group);
                    }
                }
            }
            return _context.Group != null ?
                        View(groups) :
                        Problem("Entity set 'ApplicationDbContext.Group' is null.");
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GroupName,Description,CreationDate,ProfileImage")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                //create group member 
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userProfile = await _context.UserProfile.Where(u => u.Id == userId).FirstAsync();
                var groupMember = new GroupMember
                {
                    GroupId = @group.GroupId,
                    UserProfileId = userProfile.Id,
                    IsAdmin = "true",
                    JoinDate = DateOnly.FromDateTime(DateTime.Now),
                    UserProfile = userProfile,
                    Group = @group
                };
                _context.Add(groupMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("GroupId,GroupName,Description")] Group @group, IFormFile ProfileImage)
        {
            var groupNew = await _context.Group.FindAsync(@group.GroupId);
            if (groupNew == null)
            {
                return NotFound();
            }

            try
            {
                var profileImagePath = "Images/";
                var uniqueFileName = Guid.NewGuid() + "_" + ProfileImage.FileName;
                var imagePath = Path.Combine(profileImagePath, uniqueFileName);
                var imageUrl = Path.Combine("/", imagePath);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }
                groupNew.ProfileImage = imageUrl;
                groupNew.GroupName = @group.GroupName;
                groupNew.Description = @group.Description;
                _context.Update(groupNew);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(@group.GroupId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Group == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Group'  is null.");
            }
            var @group = await _context.Group.FindAsync(id);
            if (@group != null)
            {
                _context.Group.Remove(@group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(string id)
        {
            return (_context.Group?.Any(e => e.GroupId == id)).GetValueOrDefault();
        }

    }
}
