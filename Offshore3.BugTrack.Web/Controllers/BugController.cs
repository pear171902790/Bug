using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.Models;
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
        private readonly ITransformModel _transformModel;
        private readonly IBugStatusLogic _bugStatusLogic;
        
        

        public BugController(ICookieHelper cookieHelper, IProjectLogic projectLogic, IBugLogic bugLogic, ITransformModel transformModel,IBugStatusLogic bugStatusLogic)
        {
            _cookieHelper = cookieHelper;
            _projectLogic = projectLogic;
            _bugLogic = bugLogic;
            _transformModel = transformModel;
            _bugStatusLogic = bugStatusLogic;
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
            var total=_bugLogic.GetTotal(projectId);
            var bugModels = _bugLogic.GetBugs(projectId, count, page,kw);
            var bugListViewModel=new BugListViewModel()
                {
                    ProjectId = projectId,
                    BugViewModels =_transformModel.ToBugViewModelsFromBugModels(bugModels),
                    PageViewModel = new PageViewModel(total,count,page)
                    };
            return PartialView(bugListViewModel);
        }

        public ActionResult Board(long projectId)
        {
            var statuses = _bugStatusLogic.GetAll(projectId);
            var dictionary=new Dictionary<long, string>();
            statuses.ForEach(s=>dictionary.Add(s.StatusId,s.StatusName));
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
            var project = _projectLogic.GetProjectModelWithMembers(projectId);
            var bugStatusModels = _bugStatusLogic.GetAll(projectId);
            var bugStatusNames = new List<string>();
            bugStatusModels.ForEach(bs => bugStatusNames.Add(bs.StatusName));
            var dictionary = new Dictionary<long, string>();
            project.Members.ForEach(m => dictionary.Add(m.UserId, m.UserName));
            var addEditIssueViewModel= new AddEditIssueViewModel()
            {
                Members = dictionary,
                BugStatusNames = bugStatusNames,
                ProjectId = projectId
            };
            return PartialView(addEditIssueViewModel);
        }


        public ActionResult UpdateStatusNumber(string statusNames)
        {
            try
            {
                var arr = statusNames.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    _bugStatusLogic.UpdateNumber(arr[i], i+1);
                }
                return new JsonResult() { Data = new { Status = true } };
            }
            catch (Exception)
            {
                return new JsonResult() { Data = new { Status = false } };
            }
            
        }

        [HttpPost]
        public ActionResult AddIssue([ModelBinder(typeof(JsonBinder<IssueViewModel>))]IssueViewModel issueViewModel)
        {
            var json = new JsonResult();
            try
            {
                var bugModel = _transformModel.ToBugModelFromIssueViewModel(issueViewModel);
                _bugLogic.Add(bugModel);
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
            var project = _projectLogic.GetProjectModelWithMembers(projectId);
            var bugStatusModels = _bugStatusLogic.GetAll(projectId);
            var bugStatusNames = new List<string>();
            bugStatusModels.ForEach(bs => bugStatusNames.Add(bs.StatusName));
            var dictionary = new Dictionary<long, string>();
            project.Members.ForEach(m => dictionary.Add(m.UserId, m.UserName));
            IssueViewModel issueViewModel = _transformModel.ToIssueViewModelFromBugModel(_bugLogic.Get(bugId));
            var addEditIssueViewModel =new AddEditIssueViewModel()
                {
                    Members = dictionary,
                    BugStatusNames = bugStatusNames,
                    ProjectId = projectId,
                    IssueViewModel = issueViewModel
                };
            return PartialView(addEditIssueViewModel);
        }

        [HttpPost]
        public ActionResult EditIssue([ModelBinder(typeof(JsonBinder<IssueViewModel>))]IssueViewModel issueViewModel)
        {
            var json = new JsonResult();
            try
            {
                var bugModel = _transformModel.ToBugModelFromIssueViewModel(issueViewModel);
                _bugLogic.Update(bugModel);
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
            var commentModels = _bugLogic.GetCommentModels(bugId);
            var commentViewModels = _transformModel.ToCommentViewModelsFromCommentModels(commentModels);
            return PartialView(commentViewModels);
        }

        public ActionResult BugAttachments(long bugId)
        {
            var path = Server.MapPath("~/Content/BugAttachments/" + bugId);
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
                long userId = _cookieHelper.GetUserId(Request);
                _bugLogic.AddComment(content, bugId,userId);
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
            var bugModels = _bugLogic.GetBugs(statusId);
            var bugViewModels = _transformModel.ToBugViewModelsFromBugModels(bugModels);
            return PartialView(bugViewModels);
        }
    }
}
