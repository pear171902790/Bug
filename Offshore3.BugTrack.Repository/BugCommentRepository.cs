using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class BugCommentRepository : IBugCommentRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();

       
        public void Add(BugComment bugComment)
        {
            _bugTrackDbContext.BugComments.Add(bugComment);
            _bugTrackDbContext.SaveChanges();
        }

        public List<BugComment> GetByBugId(long bugId)
        {
            return _bugTrackDbContext.BugComments.Where(c => c.BugId == bugId).ToList();
        }
    }
}
