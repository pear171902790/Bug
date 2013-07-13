using System.Collections.Generic;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ILogic
{
    public interface IBugLogic
    {
        int GetTotal(long projectId);
        Bug Get(long bugId);
        void AddComment(string content, long bugId,long userId);
        void Add(Bug bugModel);
        List<Bug> GetBugs(long projectId, int count, int page, string kw);
        void Update(Bug bugModel);
        List<Bug> GetBugs(long statusId);
        
    }
}
