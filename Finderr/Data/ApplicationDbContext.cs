using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Finderr.Models;

namespace Finderr.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserProfile>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Finderr.Models.UserProfile> UserProfile { get; set; } = default!;
        public DbSet<Finderr.Models.Group> Group { get; set; } = default!;
        public DbSet<Finderr.Models.GroupMember> GroupMember { get; set; } = default!;
        public DbSet<Finderr.Models.UserProfileTemp> UserProfileTemp { get; set; } = default!;
    }
}