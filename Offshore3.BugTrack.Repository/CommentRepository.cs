using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();

        public BugComment GetSingle(long id)
        {
            throw new NotImplementedException();
        }

        public List<BugComment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(BugComment userProjectRoleRelation)
        {
            _bugTrackDbContext.BugComments.Add(userProjectRoleRelation);
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<BugComment> Get(long bugId)
        {
            return _bugTrackDbContext.BugComments.Where(c => c.BugId == bugId).ToList();
        }
    }
}
