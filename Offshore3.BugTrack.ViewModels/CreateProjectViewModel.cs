using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ViewModels
{
    public class CreateProjectViewModel:MainLayoutViewModel
    {
        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "*")]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public override string Title
        {
            get { return "CreateProject"; }
        }
    }
}
