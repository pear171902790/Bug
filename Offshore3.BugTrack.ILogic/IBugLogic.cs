using System;
using System.Collections.Generic;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ILogic
{
    public interface IBugLogic
    {
        int GetTotal(List<long> statusIds,string kw);
        Bug Get(long bugId);
        void Add(Bug bug);
        List<Bug> GetBugs(List<long> statusIds, int count, int page, string kw);
        void Update(Bug bug);
        Bug Get(string bugName, DateTime createDate);
        List<Bug> GetList(long bugStatusId);
    }
}
