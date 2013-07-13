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

        public BugStatus GetSingle(long id)
        {
            return _bugTrackDbContext.BugStatuses.SingleOrDefault(bs => bs.BugStatusId == id);
        }

        public List<BugStatus> GetAll()
        {
            return _bugTrackDbContext.BugStatuses.ToList();
        }

        public void Add(BugStatus userProjectRoleRelation)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public BugStatus Get(string bugStatusName)
        {
            return _bugTrackDbContext.BugStatuses.SingleOrDefault(bs => bs.BugStatusName == bugStatusName);
        }

        public List<BugStatus> Get(long projectId)
        {
            return _bugTrackDbContext.BugStatuses.Where(bs => bs.ProjectId == projectId).OrderBy(bs=>bs.Number).ToList();
        }

        public void UpdateNumber(string statusName, int number)
        {
            var status = Get(statusName);
            status.Number = number;
            _bugTrackDbContext.SaveChanges();
        }
    }
}
