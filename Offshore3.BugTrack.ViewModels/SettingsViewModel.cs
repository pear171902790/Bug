using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ViewModels
{
    public class SettingsViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public string Description { get; set; }
        //public List<User> AllUsers { get; set; }
        //public List<long> SelectUserIds { get; set; }
        public string PromptInfo { get; set; }
        public long ProjectId { get; set; }
    }
}
