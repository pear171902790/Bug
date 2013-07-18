using System;
using System.Collections.Generic;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic
{
    public class BugLogic : IBugLogic
    {
        readonly IBugRepository _bugRepository;
        readonly IBugStatusRepository _bugStatusRepository;
        readonly IUserRepository _userRepository;
        readonly IProjectRepository _projectRepository;
        readonly IBugCommentRepository _bugCommentRepository;
        readonly IBugStatusLogic _bugStatusLogic;

        public BugLogic(IBugRepository bugRepository, IBugStatusRepository bugStatusRepository, IUserRepository userRepository, IProjectRepository projectRepository, IBugCommentRepository bugCommentRepository,IBugStatusLogic iBugStatusLogic)
        {
            _bugRepository = bugRepository;
            _bugStatusRepository = bugStatusRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _bugCommentRepository = bugCommentRepository;
            _bugStatusLogic = iBugStatusLogic;
        }

        public BugLogic()
        {

        }

        public int GetTotal(List<long> statusIds)
        {
            return _bugRepository.GetTotal(statusIds);
        }

        public Bug Get(long bugId)
        {
            return _bugRepository.Get(bugId);
        }


       

        public void Add(Bug bug)
        {
            _bugRepository.Add(bug);
        }

       

        public List<Bug> GetBugs(List<long> statusIds, int count, int page, string kw)
        {
            return _bugRepository.GetBugs(statusIds, count, page, kw);
        }

        public void Update(Bug bug)
        {
            _bugRepository.Update(bug);
        }

       

        public Bug Get(string bugName, DateTime createDate)
        {
            return _bugRepository.Get(bugName, createDate);
        }

        public List<Bug> GetList(long bugStatusId)
        {
            return _bugRepository.GetList(bugStatusId);

        }
    }
}