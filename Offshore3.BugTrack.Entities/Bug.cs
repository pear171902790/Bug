using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.Entities
{
    public class Bug
    {
        public long BugId { get; set; }
        public string BugName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateTime { get; set; }

        public long BugStatusId { get; set; }
        public BugStatus BugStatus { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
