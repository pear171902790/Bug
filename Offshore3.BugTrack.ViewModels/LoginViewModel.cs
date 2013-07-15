using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ViewModels
{
    public class LoginViewModel:LayoutViewModel
    {
        [Required(ErrorMessage = "*")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "format error")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string PromptInfo { get; set; }
        public override string Title {get{return "Login";}}
    }
}
