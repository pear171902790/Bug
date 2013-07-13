using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ViewModels
{
    public class UserProjectViewModel
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatDate { get; set; }
        public string Description { get; set; }
        public long RoleId { get; set; }
    }
}
