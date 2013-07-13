using System;
using System.Collections.Generic;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;
using Offshore3.BugTrack.Models;
using Offshore3.BugTrack.ViewModels;

namespace Offshore3.BugTrack.Logic
{
    public class BugLogic : IBugLogic
    {
        readonly IBugRepository _bugRepository;
        readonly ITransformModel _transformModel;
        readonly IBugStatusRepository _bugStatusRepository;
        readonly IUserRepository _userRepository;
        readonly IProjectRepository _projectRepository;
        readonly ICommentRepository _commentRepository;
        readonly IBugStatusLogic _bugStatusLogic;

        public BugLogic(IBugRepository bugRepository, ITransformModel transformModel, IBugStatusRepository bugStatusRepository, IUserRepository userRepository, IProjectRepository projectRepository, ICommentRepository commentRepository,IBugStatusLogic iBugStatusLogic)
        {
            _bugRepository = bugRepository;
            _transformModel = transformModel;
            _bugStatusRepository = bugStatusRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _commentRepository = commentRepository;
            _bugStatusLogic = iBugStatusLogic;
        }

        public BugLogic()
        {

        }

        public int GetTotal(long projectId)
        {
            return _bugRepository.GetTotal(GetStatusIds(projectId));
        }

        public List<CommentModel> GetCommentModels(long bugId)
        {
            var commentModels=new List<CommentModel>();
            var comments=_commentRepository.Get(bugId);
            comments.ForEach(c=>
                {
                    var commentModel = _transformModel.ToCommentModelFromComment(c);
                    commentModel.AffiliatedBug = Get(bugId);
                    commentModels.Add(commentModel);
                });
            return commentModels;
        }

        public BugModel Get(long bugId)
        {
            var bug = _bugRepository.GetSingle(bugId);
            bug.BugStatus = _bugStatusRepository.GetSingle(bug.BugStatusId);
            var project = _projectRepository.GetSingle(bug.BugStatus.ProjectId);
            var user = _userRepository.GetSingle(bug.UserId);
            var bugModel=_transformModel.ToBugModelFromBug(bug);
            bugModel.BugStatus =_transformModel.ToBugStatusModelFromBugStatus(bug.BugStatus);
            bugModel.AffiliatedProject = _transformModel.ToProjectModelFromProject(project);
            bugModel.Assigner = _transformModel.ToUserModelFromUser(user);
            return bugModel;
        }

        public void AddComment(string content, long bugId,long userId)
        {
            var comment = new BugComment()
                {
                    Content = content,
                    AddTime = DateTime.Now,
                    BugId = bugId,
                    Commentator = _userRepository.GetSingle(userId).UserName
                };
            _commentRepository.Add(comment);
        }

        public void Add(BugModel bugModel)
        {
            if (_bugStatusLogic.IsNewStatus(bugModel.BugStatus.StatusName))
            {
                _bugStatusLogic.Add(bugModel.BugStatus.StatusName, bugModel.AffiliatedProject.ProjectId);
            }
            var statusId = _bugStatusRepository.Get(bugModel.BugStatus.StatusName).BugStatusId;
            var bug = _transformModel.ToBugFromBugModel(bugModel);
            bug.BugStatusId = statusId;
            _bugRepository.Add(bug);
        }

        private List<long> GetStatusIds(long projectId)
        {
            var statuses = _bugStatusRepository.Get(projectId);
            var statusIds = new List<long>();
            statuses.ForEach(bs => statusIds.Add(bs.BugStatusId));
            return statusIds;
        }

        public List<BugModel> GetBugs(long projectId, int count, int page,string kw)
        {
            var bugModels=new List<BugModel>();
            List<Bug> bugs = _bugRepository.GetBugs(GetStatusIds(projectId), count, page, kw);
            bugs.ForEach(b=>bugModels.Add(Get(b.BugId)));
            return bugModels;
        }

        public void Update(BugModel bugModel)
        {
            if (_bugStatusLogic.IsNewStatus(bugModel.BugStatus.StatusName))
            {
                _bugStatusLogic.Add(bugModel.BugStatus.StatusName,bugModel.AffiliatedProject.ProjectId);
            }
            var statusId = _bugStatusRepository.Get(bugModel.BugStatus.StatusName).BugStatusId;
            var bug = _transformModel.ToBugFromBugModel(bugModel);
            bug.BugStatusId = statusId;
            _bugRepository.Update(bug);
        }

        public List<BugModel> GetBugs(long statusId)
        {
            var bugModels = new List<BugModel>();
            var bugs=_bugRepository.GetBugs(statusId);
            bugs.ForEach(b => bugModels.Add(Get(b.BugId)));
            return bugModels;
        }

        
    }
}