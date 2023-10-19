using Microsoft.AspNetCore.Mvc;

namespace Finderr.Models.ViewModels
{
    public class UserProfileView : UserProfile
    {
        [BindProperty]
        public IFormFile? ProfileImage { get; set; }
    }
}
