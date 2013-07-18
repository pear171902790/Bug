using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic
{
    public class BugCommentLogic : IBugCommentLogic
    {
        readonly IBugCommentRepository _bugCommentRepository;

        public BugCommentLogic(IBugCommentRepository bugCommentRepository)
        {
            _bugCommentRepository = bugCommentRepository;
        }
        public List<BugComment> GetByBugId(long bugId)
        {
            return _bugCommentRepository.GetByBugId(bugId);
        }

        public void Add(BugComment bugComment)
        {
            _bugCommentRepository.Add(bugComment);
        }
    }
}
