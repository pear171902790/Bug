using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.Entities
{
    public class BugStatus
    {
        public virtual long BugStatusId { get; set; }
        public virtual string BugStatusName { get; set; }
        public virtual int Number { get; set; }
        public virtual long ProjectId { get; set; }
        
        public Project Project { get; set; }
    }
}
