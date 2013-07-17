using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class DetailsViewModel:MainLayoutViewModel
    {
        public long ProjectId { get; set; }

        public override string Title
        {
            get { return "Project Details"; }
        }
    }
}
