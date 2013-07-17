using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.ViewModels;
using Offshore3.BugTrack.Web.Common;

namespace Offshore3.BugTrack.Web.Controllers
{
    [CustomAuthorize]
    public class BugController : Controller
    {
        private readonly ICookieHelper _cookieHelper;
        private readonly IProjectLogic _projectLogic;
        private readonly IBugLogic _bugLogic;
        private readonly IBugStatusLogic _bugStatusLogic;
        private readonly IUserProjectRoleRelationLogic _userProjectRoleRelationLogic;
        private readonly IUserLogic _userLogic;
        private readonly IBugCommentLogic _bugCommentLogic;

        public BugController(ICookieHelper cookieHelper, IProjectLogic projectLogic, IBugLogic bugLogic, IBugStatusLogic bugStatusLogic, IUserProjectRoleRelationLogic userProjectRoleRelationLogic, IUserLogic userLogic, IBugCommentLogic bugCommentLogic)
        {
            _cookieHelper = cookieHelper;
            _projectLogic = projectLogic;
            _bugLogic = bugLogic;
            _bugStatusLogic = bugStatusLogic;
            _userProjectRoleRelationLogic = userProjectRoleRelationLogic;
            _userLogic = userLogic;
            _bugCommentLogic = bugCommentLogic;
        }


        public ActionResult Issues(long projectId)
        {
            return PartialView(projectId);
        }

        public ActionResult Grid(long projectId)
        {
            return PartialView(projectId);
        }

        public ActionResult BugList(long projectId,int count,int page,string kw)
        {
            var bugStatuses = _bugStatusLogic.GetList(projectId);
            var statusIds=new List<long>();
            bugStatuses.ForEach(bs => statusIds.Add(bs.BugStatusId));
            var total = _bugLogic.GetTotal(statusIds,kw);
            var bugs = _bugLogic.GetBugs(statusIds, count, page, kw);
            var bugViewModels=new List<BugViewModel>();
            bugs.ForEach(b =>
                {
                    var bugViewModel=new BugViewModel
                        {
                            BugId = b.BugId,
                            BugName = b.BugName,
                            Description = b.Description,
                            BugStatusId = b.BugStatusId,
                            BugStatusName = _bugStatusLogic.Get(b.BugStatusId).BugStatusName,
                            AssignerId = b.UserId,
                            AssignerName = _userLogic.Get(b.UserId).UserName,
                            CreateDate = b.CreateDate,
                            UpdateTime = b.UpdateTime,
                        };
                    bugViewModels.Add(bugViewModel);
                });
            var bugListViewModel=new BugListViewModel()
                {
                    ProjectId = projectId,
                    BugViewModels = bugViewModels,
                    PageViewModel = new PageViewModel(total,count,page)
                    };
            return PartialView(bugListViewModel);
        }

        public ActionResult Board(long projectId)
        {
            var statuses = _bugStatusLogic.GetList(projectId);
            var dictionary=new Dictionary<long, string>();
            statuses.ForEach(s=>dictionary.Add(s.BugStatusId,s.BugStatusName));
            var boardViewModel=new BoardViewModel
                {
                    ProjectId = projectId,
                    Statuses = dictionary
                };
            return PartialView(boardViewModel);
        }

        [HttpGet]
        public ActionResult AddIssue(long projectId)
        {
            var bugStatusNames = new List<string>();
            _bugStatusLogic.GetList(projectId).ForEach(status => bugStatusNames.Add(status.BugStatusName));
            var dictionary = new Dictionary<long, string>();
            var userProjectRoleRelations =_userProjectRoleRelationLogic.GetByProjectId(projectId);
            userProjectRoleRelations.ForEach(uprr =>
                {
                    var user = _userLogic.Get(uprr.UserId);
                    dictionary.Add(user.UserId,user.UserName);
                });
            var addEditIssueViewModel= new AddEditBugViewModel()
            {
                Members = dictionary,
                BugStatusNames = bugStatusNames,
                ProjectId = projectId
            };
            return PartialView(addEditIssueViewModel);
        }


        public ActionResult UpdateStatusNumber(string statusIds)
        {
            try
            {
                var arr = statusIds.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    _bugStatusLogic.UpdateNumber(Convert.ToInt64(arr[i]), i+1);
                }
                return new JsonResult() { Data = new { Status = true } };
            }
            catch (Exception)
            {
                return new JsonResult() { Data = new { Status = false } };
            }
        }

        [HttpPost]
        public ActionResult AddIssue([ModelBinder(typeof(JsonBinder<BugViewModel>))]BugViewModel bugViewModel)
        {
            var json = new JsonResult();
            try
            {
                var bugStatus = _bugStatusLogic.Get(bugViewModel.BugStatusName,bugViewModel.ProjectId);
                if (bugStatus == null)
                {
                    bugStatus = new BugStatus
                    {
                        BugStatusName = bugViewModel.BugStatusName,
                        ProjectId = bugViewModel.ProjectId,
                        Number = 0
                    };
                    _bugStatusLogic.Add(bugStatus);
                    bugStatus = _bugStatusLogic.Get(bugViewModel.BugStatusName, bugViewModel.ProjectId);
                }
                var bug = new Bug
                    {
                        BugName = bugViewModel.BugName,
                        Description = bugViewModel.Description,
                        UserId = bugViewModel.AssignerId,
                        BugStatusId = bugStatus.BugStatusId,
                        CreateDate = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };
                _bugLogic.Add(bug);
                var bugId = _bugLogic.Get(bug.BugName, bug.CreateDate).BugId;
                var userId = _cookieHelper.GetUserId(Request);
                var ioPath = Server.MapPath(Url.Content("~/Content/BugAttachments/"));
                var bugAttachmentsPath = ioPath + userId + "_" + bugId;
                var tempPath = ioPath + userId + "_temp";
                System.IO.Directory.Move(tempPath,bugAttachmentsPath);
                json.Data = new { Status = true };
            }
            catch
            {
                json.Data = new { Status = false };
            }
            return json;
        }

        [HttpGet]
        public ActionResult EditIssue(long projectId, long bugId)
        {
            var bugStatusNames = new List<string>();
            _bugStatusLogic.GetList(projectId).ForEach(status => bugStatusNames.Add(status.BugStatusName));
            var dictionary = new Dictionary<long, string>();
            var userProjectRoleRelations = _userProjectRoleRelationLogic.GetByProjectId(projectId);
            userProjectRoleRelations.ForEach(uprr =>
            {
                var user = _userLogic.Get(uprr.UserId);
                dictionary.Add(user.UserId, user.UserName);
            });
            var bug = _bugLogic.Get(bugId);
            var bugViewModel =new BugViewModel()
                {
                    BugId = bug.BugId,
                    BugName = bug.BugName,
                    Description = bug.Description??string.Empty,
                    BugStatusName = _bugStatusLogic.Get(bug.BugStatusId).BugStatusName,
                    AssignerId = bug.UserId,
                    ProjectId = projectId
                };
            var addEditBugViewModel =new AddEditBugViewModel()
                {
                    Members = dictionary,
                    BugStatusNames = bugStatusNames,
                    ProjectId = projectId,
                    BugViewModel = bugViewModel
                };
            return PartialView(addEditBugViewModel);
        }

        [HttpPost]
        public ActionResult EditIssue([ModelBinder(typeof(JsonBinder<BugViewModel>))]BugViewModel bugViewModel)
        {
            var json = new JsonResult();
            try
            {
                var bugStatus = _bugStatusLogic.Get(bugViewModel.BugStatusName,bugViewModel.ProjectId);
                if (bugStatus == null)
                {
                    bugStatus = new BugStatus
                    {
                        BugStatusName = bugViewModel.BugStatusName,
                        ProjectId = bugViewModel.ProjectId,
                        Number = 0
                    };
                    _bugStatusLogic.Add(bugStatus);
                    bugStatus = _bugStatusLogic.Get(bugViewModel.BugStatusName, bugViewModel.ProjectId);
                }
                var bug=new Bug
                    {
                        BugId = bugViewModel.BugId,
                        BugName = bugViewModel.BugName,
                        Description = bugViewModel.Description,
                        BugStatusId = bugStatus.BugStatusId,
                        UpdateTime = DateTime.Now,
                        UserId = bugViewModel.AssignerId
                    };
                _bugLogic.Update(bug);
                json.Data = new { Status = true };
            }
            catch
            {
                json.Data = new { Status = false };
            }
            return json;
        }

        public ActionResult Comments(long bugId)
        {
            var bugComments = _bugCommentLogic.GetByBugId(bugId);
            var commentViewModels =new List<CommentViewModel>(); 
            bugComments.ForEach(bc =>
                {
                    var comment=new CommentViewModel
                        {
                            Content = bc.Content,
                            Commentator = bc.Commentator,
                            AddTime = bc.AddTime
                        };
                    commentViewModels.Add(comment);
                });
            return PartialView(commentViewModels);
        }

        public ActionResult BugAttachments(long bugId)
        {
            var userId = _cookieHelper.GetUserId(Request);
            var folderName = bugId == 0 ? userId + "_temp" : userId + "_" + bugId;
            var path = Server.MapPath("~/Content/BugAttachments/" + folderName);
            var attachmentNames = new List<string>();
            if (System.IO.Directory.Exists(path))
            {
                var fullPaths = System.IO.Directory.GetFiles(path);
                foreach (var fullPath in fullPaths)
                {
                    attachmentNames.Add(System.IO.Path.GetFileName(fullPath));
                }
            }
            var bugAttachmentsViewModel = new BugAttachmentsViewModel()
                {
                    BugId = bugId,
                    AttachmentNames = attachmentNames
                };
            return PartialView(bugAttachmentsViewModel);
        }

        public ActionResult AddComment(string content, long bugId)
        {
            var jsonResult = new JsonResult();
            try
            {
                var userId = _cookieHelper.GetUserId(Request);
                var user = _userLogic.Get(userId);
                var bugComment=new BugComment
                    {
                        Commentator = user.UserName,
                        Content = content,
                        BugId = bugId,
                        AddTime = DateTime.Now
                    };
                _bugCommentLogic.Add(bugComment);
                jsonResult.Data = new {Status = true};
            }
            catch (Exception)
            {
                jsonResult.Data = new { Status = false };
            }
            return jsonResult;
        }

        public ActionResult BugListing(long projectId, long statusId)
        {

            var bugs = _bugLogic.GetList(statusId);
            var bugViewModels = new List<BugViewModel>();
            bugs.ForEach(b =>
            {
                var bugViewModel = new BugViewModel
                {
                    BugId = b.BugId,
                    BugName = b.BugName,
                    Description = b.Description,
                    BugStatusId = b.BugStatusId,
                    BugStatusName = _bugStatusLogic.Get(b.BugStatusId).BugStatusName,
                    AssignerId = b.UserId,
                    AssignerName = _userLogic.Get(b.UserId).UserName,
                    CreateDate = b.CreateDate,
                    UpdateTime = b.UpdateTime,
                };
                bugViewModels.Add(bugViewModel);
            });
            return PartialView(bugViewModels);
        }
    }
}
