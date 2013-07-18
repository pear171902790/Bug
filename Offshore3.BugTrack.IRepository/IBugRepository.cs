using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IBugRepository
    {
        void Add(Bug bug);
        List<Bug> GetBugs(List<long> statusIds, long userId);
        int GetTotal(List<long> statusIds);
        List<Bug> GetBugs(List<long> statusIds, int count,int page,string kw);
        void Update(Bug bug);
        List<Bug> GetList(long bugStatusId);
        Bug Get(string bugName, DateTime createDate);
        Bug Get(long bugId);
    }
}
