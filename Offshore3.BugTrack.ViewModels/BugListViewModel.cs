using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ViewModels
{
    public class BugListViewModel
    {
        public List<BugViewModel> BugViewModels { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public long  ProjectId { get; set; }
    }
}
