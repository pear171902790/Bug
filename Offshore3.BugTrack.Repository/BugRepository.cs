using System;
using System.Collections.Generic;
using System.Linq;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class BugRepository : IBugRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();

        public List<Bug> GetBugs(List<long> statusIds, long userId)
        {
            return  _bugTrackDbContext.Bugs.
                   Where(b => statusIds.Contains(b.BugStatusId) && b.UserId == userId).ToList();
        }

        public int GetTotal(List<long> statusIds)
        {
            return _bugTrackDbContext.Bugs.Count(b => statusIds.Contains(b.BugStatusId));
        }

        public List<Bug> GetBugs(List<long> statusIds , int count, int page,string kw)
        {
            var bugs=new List<Bug>();
            var skipCount = (page - 1)*count;
            if (string.IsNullOrEmpty(kw))
            {
                bugs = _bugTrackDbContext.Bugs.Where(b => statusIds.Contains(b.BugStatusId)).OrderByDescending(b => b.UpdateTime).Skip(skipCount).Take(count).ToList();
            }
            else
            {
                bugs = _bugTrackDbContext.Bugs.Where
                        (
                            b => statusIds.Contains(b.BugStatusId) &&
                                (b.BugName.Contains(kw)||b.Description.Contains(kw))
                        ).OrderByDescending(b => b.UpdateTime).Skip(skipCount).Take(count).ToList();
            }
            return bugs;
        }

        public void Update(Bug bug)
        {
            var single = GetSingle(bug.BugId);
            single.BugName = bug.BugName;
            single.UserId = bug.UserId;
            single.UpdateTime = bug.UpdateTime;
            single.BugStatusId = bug.BugStatusId;
            single.Description = bug.Description;
            _bugTrackDbContext.SaveChanges();
        }

        public List<Bug> GetBugs(long statusId)
        {
            var bugs= _bugTrackDbContext.Bugs.Where(
                b => b.BugStatusId == statusId
                ).ToList();
            return bugs;
        }

        public Bug GetSingle(long id)
        {
            return _bugTrackDbContext.Bugs.SingleOrDefault(b => b.BugId == id);
        }

        public List<Bug> GetAll()
        {
            return _bugTrackDbContext.Bugs.ToList();
        }

        public void Add(Bug userProjectRoleRelation)
        {
            userProjectRoleRelation.BugId = 0;
            _bugTrackDbContext.Bugs.Add(userProjectRoleRelation);
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            _bugTrackDbContext.Bugs.Remove(GetSingle(id));
            _bugTrackDbContext.SaveChanges();
        }
    }
}