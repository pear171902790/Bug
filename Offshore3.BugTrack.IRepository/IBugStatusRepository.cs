using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IBugStatusRepository
    {
        BugStatus Get(long bugStatusId);
        BugStatus Get(string bugStatusName, long projectId);
        void Add(BugStatus bugStatus);
        void UpdateNumber(long statusId, int number);
        List<BugStatus> GetList(long projectId);
    }
}
