using Finderr.Data;
using Finderr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Finderr.Controllers
{
    public class GroupMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GroupMembers
        public async Task<IActionResult> Index(string groupId)
        {
            if (groupId == null || _context.GroupMember == null)
            {
                return NotFound();
            }
            var applicationDbContext = await _context.GroupMember
                .Include(g => g.Group)
                .Include(g => g.UserProfile)
                .Where(g => g.GroupId == groupId).ToListAsync();
            var groupMemberView = new List<GroupMember>();
            foreach (var groupMember in applicationDbContext)
            {
                var userProfile = await _context.UserProfile.Where(u => u.Id == groupMember.UserProfileId).FirstAsync();
                groupMember.UserProfile = userProfile;
                groupMemberView.Add(groupMember);
            }
            return View(groupMemberView);
        }

        // GET: GroupMembers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GroupMember == null)
            {
                return NotFound();
            }

            var groupMember = await _context.GroupMember
                .Include(g => g.Group)
                .Include(g => g.UserProfile)
                .FirstOrDefaultAsync(m => m.GroupMemberId == id);
            if (groupMember == null)
            {
                return NotFound();
            }

            return View(groupMember);
        }

        // GET: GroupMembers/Create
        public IActionResult Create()
        {
            ViewData["IsAdmin"] = new SelectList(new List<string> { "true", "false" });
            ViewData["GroupId"] = new SelectList(_context.Group, "GroupId", "GroupName");
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "Id", "Name");
            return View();
        }

        // POST: GroupMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupMemberId,IsAdmin,UserProfileId,GroupId")] GroupMember groupMember)
        {
            groupMember.GroupMemberId = Guid.NewGuid().ToString();
            groupMember.JoinDate = DateOnly.FromDateTime(DateTime.Now);
            _context.Add(groupMember);
            int v = await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { groupId = groupMember.GroupId });

        }

        // GET: GroupMembers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GroupMember == null)
            {
                return NotFound();
            }

            var groupMember = await _context.GroupMember.FindAsync(id);
            if (groupMember == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "GroupId", "GroupId", groupMember.GroupId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "Id", "Id", groupMember.UserProfileId);
            return View(groupMember);
        }

        // POST: GroupMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GroupMemberId,IsAdmin,JoinDate,UserProfileId,GroupId")] GroupMember groupMember)
        {
            if (id != groupMember.GroupMemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupMemberExists(groupMember.GroupMemberId))
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
            ViewData["GroupId"] = new SelectList(_context.Group, "GroupId", "GroupId", groupMember.GroupId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "Id", "Id", groupMember.UserProfileId);
            return View(groupMember);
        }

        // GET: GroupMembers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GroupMember == null)
            {
                return NotFound();
            }

            var groupMember = await _context.GroupMember
                .Include(g => g.Group)
                .Include(g => g.UserProfile)
                .FirstOrDefaultAsync(m => m.GroupMemberId == id);
            if (groupMember == null)
            {
                return NotFound();
            }

            return View(groupMember);
        }

        // POST: GroupMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GroupMember == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GroupMember'  is null.");
            }
            var groupMember = await _context.GroupMember.FindAsync(id);
            if (groupMember != null)
            {
                _context.GroupMember.Remove(groupMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupMemberExists(string id)
        {
            return (_context.GroupMember?.Any(e => e.GroupMemberId == id)).GetValueOrDefault();
        }
    }
}
