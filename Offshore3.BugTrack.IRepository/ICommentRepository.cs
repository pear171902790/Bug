using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface ICommentRepository:IRepository<BugComment>
    {
        List<BugComment> Get(long bugId);

    }
}
