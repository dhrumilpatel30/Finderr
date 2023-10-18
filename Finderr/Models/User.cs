using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finderr.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Column("name", TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column("occupation", TypeName = "varchar(100)")]
        public string Occupation { get; set; }
        [Required]
        [Column("current_address", TypeName = "varchar(150)")]
        public string CurrentAddress { get; set; }
        [Required]
        [Column("profile_privacy", TypeName = "bit")]
        public bool ProfilePrivacy { get; set; }
        [Required]
        [Column("profile_picture_reference", TypeName = "varchar(150)")]
        public string ProfilePictureReference { get; set; }
        [Column("facebook_link", TypeName = "varchar(255)")]
        public string? FacebookLink { get; set; }
        [Column("twitter_link", TypeName = "varchar(255)")]
        public string? TwitterLink { get; set; }
        [Column("linkedin_link", TypeName = "varchar(255)")]
        public string? LinkedInLink { get; set; }


        public User()
        {
            Name = "John Doe";
            Occupation = "Not set";
            CurrentAddress = "Not set";
            ProfilePrivacy = false;
            ProfilePictureReference = "https://img.freepik.com/free-psd/3d-icon-social-media-app_23-2150049569.jpg";
        }
    }

}
