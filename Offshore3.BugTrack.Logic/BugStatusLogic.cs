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
        private readonly ITransformModel _transformModel;
        public BugStatusLogic(IBugStatusRepository bugStatusRepository,ITransformModel transformModel)
        {
            _bugStatusRepository = bugStatusRepository;
            _transformModel = transformModel;
        }

        public BugStatusModel Get(long bugStatusId)
        {
            return _transformModel.ToBugStatusModelFromBugStatus(_bugStatusRepository.GetSingle(bugStatusId));
        }

        public List<BugStatusModel> GetAll(long projectId)
        {
            var bugStatusModels = new List<BugStatusModel>();
            var bugStatuses = _bugStatusRepository.Get(projectId);
            bugStatuses.ForEach(bs => bugStatusModels.Add(_transformModel.ToBugStatusModelFromBugStatus(bs)));
            return bugStatusModels;
        }

        public BugStatusModel Get(string bugStatusName)
        {
            return _transformModel.ToBugStatusModelFromBugStatus(_bugStatusRepository.Get(bugStatusName));
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
    }
}
