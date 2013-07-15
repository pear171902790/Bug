using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class IndexViewModel : MainLayoutViewModel
    {
        public List<ProjectViewModel> ProjectViewModels { get; set; }

        public override string Title
        {
            get { return "Index"; }
        }
    }
}
