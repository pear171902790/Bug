using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class BugStatusRepository : IBugStatusRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();

        public BugStatus Get(long bugStatusId)
        {
            return _bugTrackDbContext.BugStatuses.SingleOrDefault(bs => bs.BugStatusId == bugStatusId);
        }

        public List<BugStatus> GetAll()
        {
            return _bugTrackDbContext.BugStatuses.ToList();
        }

        public void Add(BugStatus bugStatus)
        {
            _bugTrackDbContext.BugStatuses.Add(bugStatus);
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public BugStatus Get(string bugStatusName, long projectId)
        {
            return _bugTrackDbContext.BugStatuses.SingleOrDefault(bs => bs.BugStatusName == bugStatusName&&bs.ProjectId==projectId);
        }

        public void UpdateNumber(long statusId, int number)
        {
            var status = Get(statusId);
            status.Number = number;
            _bugTrackDbContext.SaveChanges();
        }

        public List<BugStatus> GetList(long projectId)
        {
            return _bugTrackDbContext.BugStatuses.Where(bs => bs.ProjectId == projectId).OrderBy(s=>s.Number).ToList();
        }
    }
}
