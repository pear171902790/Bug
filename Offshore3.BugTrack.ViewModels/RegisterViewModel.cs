using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ViewModels
{
    public class RegisterViewModel:LayoutViewModel
    {
        [Required(ErrorMessage = "*")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "(3-100)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, MinimumLength = 6,ErrorMessage = "(6-100)")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Must match password.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "format error")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string PromptInfo { get; set; }
        public override string Title { get { return "Register"; } }


    }
}
