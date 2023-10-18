using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finderr.Models
{
    public class Group
    {
        [Key]
        [Column("group_id", TypeName = "varchar(255)")]
        public string GroupId { get; set; }

        [Required]
        [Column("group_name", TypeName = "varchar(100)")]
        public string GroupName { get; set; }

        [Required]
        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; }

        [Required]
        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("profile_image", TypeName = "varchar(150)")]
        public string ProfileImage { get; set; }
        
        public Group() { 
            CreationDate = DateTime.Now;
            GroupName = "Not provided";
            Description = "Not provided";
            ProfileImage = "https://img.freepik.com/free-psd/3d-icon-social-media-app_23-2150049569.jpg";
            GroupId = Guid.NewGuid().ToString();
        }
        public Group(string GroupName)
        {
            CreationDate = DateTime.Now;
            this.GroupName = GroupName;
            Description = "Not provided";
            ProfileImage = "https://img.freepik.com/free-psd/3d-icon-social-media-app_23-2150049569.jpg";
            GroupId = Guid.NewGuid().ToString();
        }
    }
}
