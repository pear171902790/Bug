using System.Collections.Generic;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ILogic
{
    public interface IBugStatusLogic
    {
        BugStatus Get(long bugStatusId);
        BugStatus Get(string bugStatusName,long projectId);
        void UpdateNumber(long statusId, int number);
        void Add(BugStatus bugStatus);
        List<BugStatus> GetList(long projectId);
    }
}
