using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IBugStatusRepository:IRepository<BugStatus>
    {
        BugStatus Get(string bugStatusName);
        List<BugStatus> Get(long projectId);
        void UpdateNumber(string statusName, int number);
    }
}
