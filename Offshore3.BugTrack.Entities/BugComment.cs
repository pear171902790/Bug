using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.Entities
{
    public class BugComment
    {
        public virtual long BugCommentId { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime AddTime { get; set; }

        public virtual long BugId { get; set; }
        public virtual Bug Bug { get; set; }
        public string  Commentator { get; set; }

    }
}
