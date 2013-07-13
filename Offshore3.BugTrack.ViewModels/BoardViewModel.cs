using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class BoardViewModel
    {
        public Dictionary<long, string> Statuses { get; set; }
        public long ProjectId { get; set; }
    }
}
