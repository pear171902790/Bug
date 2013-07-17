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
        BugStatus Get(string bugStatusName);
        void Add(BugStatus bugStatus);
        void UpdateNumber(string statusName, int number);
    }
}
