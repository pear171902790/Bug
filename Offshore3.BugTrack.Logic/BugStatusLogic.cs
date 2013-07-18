using System.Collections.Generic;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic
{
    public class BugStatusLogic : IBugStatusLogic
    {
        private readonly IBugStatusRepository _bugStatusRepository;
        public BugStatusLogic(IBugStatusRepository bugStatusRepository)
        {
            _bugStatusRepository = bugStatusRepository;
        }

        public BugStatus Get(long bugStatusId)
        {
            return _bugStatusRepository.Get(bugStatusId);
        }


        public BugStatus Get(string bugStatusName, long projectId)
        {
            return _bugStatusRepository.Get(bugStatusName,projectId);
        }

       

        public void UpdateNumber(long statusId, int number)
        {
            _bugStatusRepository.UpdateNumber(statusId, number);
        }

        public void Add(BugStatus bugStatus)
        {
            _bugStatusRepository.Add(bugStatus);
        }

        public List<BugStatus> GetList(long projectId)
        {
            return _bugStatusRepository.GetList(projectId);
        }
    }
}
