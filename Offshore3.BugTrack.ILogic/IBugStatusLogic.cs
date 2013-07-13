using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.Models;

namespace Offshore3.BugTrack.ILogic
{
    public interface IBugStatusLogic
    {
        BugStatus Get(long bugStatusId);
        List<BugStatus> GetAll(long projectId);
        BugStatus Get(string bugStatusName);
        bool IsNewStatus(string bugStatusName);
        void Add(string bugStatusName, long projectId);
        void UpdateNumber(string statusName, int number);
    }
}
