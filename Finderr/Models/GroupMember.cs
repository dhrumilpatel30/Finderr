using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finderr.Models
{
    public class GroupMember
    {
        [Key]
        public string GroupMemberId { get; set; }
        [Required]
        [Column("is_admin", TypeName = "varchar(10)")]
        public string IsAdmin { get; set; }
        [Required]
        [Column("join_date")]
        public DateOnly JoinDate { get; set; }
        [ForeignKey("UserProfileId")]
        public string UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }

        [ForeignKey("GroupId")]
        public string GroupId { get; set; }
        public Group? Group { get; set; }

        public GroupMember()
        {
            IsAdmin = "false";
            JoinDate = new DateOnly();
            UserProfileId = "";
            GroupId = "";
            GroupMemberId = Guid.NewGuid().ToString();
        }
    }
}
