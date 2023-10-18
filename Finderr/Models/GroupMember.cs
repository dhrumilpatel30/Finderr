using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finderr.Models
{
    public class GroupMember
    {
        [Key]
        public int GroupMemberId { get; set; }
        [Required]
        [Column("is_admin", TypeName = "bit")]
        public bool IsAdmin { get; set; }
        [Required]
        [Column("join_date")]
        public DateOnly JoinDate { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("GroupId")]
        public string GroupId { get; set; }
        public Group Group { get; set; }
    }
}
