using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class InitViewModel:LayoutViewModel
    {
        public override string Title
        {
            get { return "Init"; }
        }

        public string UserName { get; set; }
    }
}
