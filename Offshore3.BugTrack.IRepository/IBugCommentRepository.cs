using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IBugCommentRepository
    {
        List<BugComment> GetByBugId(long bugId);
        void Add(BugComment bugComment);
    }
}
