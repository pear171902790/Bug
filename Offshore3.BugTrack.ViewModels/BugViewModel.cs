using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ViewModels
{
    public class BugViewModel
    {
        public long BugId { get; set; }
        public string BugName { get; set; }
        public string  Description{ get; set; }
        public string BugStatusName { get; set; }
        public string AssignerName { get; set; }
        public long AssignerId { get; set; }
        public long BugStatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
