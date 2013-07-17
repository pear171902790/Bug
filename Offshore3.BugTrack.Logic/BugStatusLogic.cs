using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;
using Offshore3.BugTrack.Models;

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

        List<BugStatus> IBugStatusLogic.GetAll(long projectId)
        {
            throw new NotImplementedException();
        }

        public BugStatus Get(string bugStatusName)
        {
            return _bugStatusRepository.Get(bugStatusName);
        }

        public List<BugStatusModel> GetAll(long projectId)
        {
            var bugStatusModels = new List<BugStatusModel>();
            var bugStatuses = _bugStatusRepository.Get(projectId);
            bugStatuses.ForEach(bs => bugStatusModels.Add(_transformModel.ToBugStatusModelFromBugStatus(bs)));
            return bugStatusModels;
        }

        

        public bool IsNewStatus(string bugStatusName)
        {
            return _bugStatusRepository.Get(bugStatusName) == null;
        }

        public void Add(string bugStatusName,long projectId)
        {
            _bugStatusRepository.Add(new BugStatus { BugStatusName = bugStatusName, ProjectId = projectId });
        }

        public void UpdateNumber(string statusName, int number)
        {
            _bugStatusRepository.UpdateNumber(statusName, number);
        }

        public void Add(BugStatus bugStatus)
        {
            _bugStatusRepository.Add(bugStatus);
        }
    }
}
