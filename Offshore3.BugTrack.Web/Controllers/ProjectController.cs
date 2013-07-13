using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.Models;
using Offshore3.BugTrack.ViewModels;
using Offshore3.BugTrack.Web.Common;

namespace Offshore3.BugTrack.Web.Controllers
{
    [CustomAuthorize]
    public class ProjectController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IProjectLogic _projectLogic;
        private readonly ICookieHelper _cookieHelper;
        private readonly ITransformModel _transformModel;

        public ProjectController
            (
                IUserLogic userLogic,
                IProjectLogic projectLogic, 
                ICookieHelper cookieHelper,
                ITransformModel transformModel
            )
        {
            _userLogic = userLogic;
            _projectLogic = projectLogic;
            _cookieHelper = cookieHelper;
            _transformModel = transformModel;
        }



        public ActionResult MemberList(long projectId)
        {
            var roleId = _projectLogic.GetRoleId(projectId, _cookieHelper.GetUserId(Request));
            var projectModel = _projectLogic.GetProjectModelWithMembers(projectId);
            var memberViewModels = _transformModel.ToMemberViewModelsFromProjectModel(projectModel);
            var memberListViewModel=new MemberListViewModel()
                {
                    ProjectId = projectId,
                    MemberViewModels = memberViewModels,
                    CurrentUserRoleId = roleId
                };
            return PartialView(memberListViewModel);
        }

        public ActionResult MemberManager(long projectId)
        {
            return PartialView(projectId);
        }
        
        public ActionResult Details(long id)
        {
            var userId=_cookieHelper.GetUserId(Request);
            ViewBag.CurrentUserName = _userLogic.Get(userId).UserName;
            return View(id);
        }

        public ActionResult Settings(long projectId)
        {
            var projectModel = _projectLogic.Get(projectId);
            var settingsViewModel = _transformModel.ToSettingsViewModelFromProjectModel(projectModel);
            return PartialView(settingsViewModel);
        }
        
        public ActionResult Update([ModelBinder(typeof(JsonBinder<SettingsViewModel>))]SettingsViewModel settingsViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                var projectModel = _transformModel.ToProjectModelFromSettingsViewModel(settingsViewModel);
                _projectLogic.Update(projectModel);

                jsonResult.Data = new { IsSuccess = true };
            }
            catch (Exception)
            {
                jsonResult.Data = new { IsSuccess = false };
            }
            return jsonResult;
        }

        public ActionResult UpdateRoleRelation(MemberListViewModel memberListViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                _projectLogic.UpdateRoleRelation(memberListViewModel.ProjectId, memberListViewModel.UserId, memberListViewModel.RoleId);
                jsonResult.Data = new { IsSuccess = true };

            }
            catch (Exception)
            {
                jsonResult.Data = new {IsSuccess = false};
            }
            return jsonResult;
        }
        

        public ActionResult AddMember(MemberManagerViewModel memberManagerViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                var userModel = _userLogic.Get(memberManagerViewModel.UserName);
                if (userModel != null)
                {
                    if (_projectLogic.CheckIsMember(memberManagerViewModel.ProjectId,userModel.UserId))
                    {
                        jsonResult.Data = new { IsSuccess = false, ErrorMessage = "Already have this member" };
                    }
                    else
                    {
                        _projectLogic.AddMember(userModel.UserId, memberManagerViewModel.ProjectId, memberManagerViewModel.RoleId);
                        jsonResult.Data = new { IsSuccess = true, ErrorMessage = "add success" };
                    }
                }
                else
                {
                    jsonResult.Data = new { IsSuccess = false, ErrorMessage = "Without the user" };
                }
            }
            catch (Exception)
            {
                jsonResult.Data = new { IsSuccess = false, ErrorMessage = "Add failed" };
            }
            return jsonResult;
        }

        public ActionResult DeleteMember(MemberListViewModel memberListViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                _projectLogic.DeleteMember(memberListViewModel.ProjectId, memberListViewModel.UserId);
                jsonResult.Data = new { IsSuccess = true };

            }
            catch (Exception)
            {
                jsonResult.Data = new { IsSuccess = false };
            }
            return jsonResult;
        }
    }
}