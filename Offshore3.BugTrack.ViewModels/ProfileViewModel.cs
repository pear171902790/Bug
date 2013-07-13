using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "*")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "(3-100)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "(6-100)")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Must match password.")]
        [Display(Name = "Repeat Password")]
        public string RepeatPassword { get; set; }

        public bool Gender { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string Introduction { get; set; }

    }
}
