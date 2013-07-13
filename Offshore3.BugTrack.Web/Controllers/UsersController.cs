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
    public class UsersController : Controller
    {
        private readonly ICookieHelper _cookieHelper;
        private readonly ITransformModel _transformModel;
        private readonly IUserLogic _userLogic;
        private readonly IUserProjectRoleRelationLogic _userProjectRoleRelationLogic;



        public UsersController(IUserLogic userLogic, ICookieHelper cookieHelper, ITransformModel transformModel, IUserProjectRoleRelationLogic userProjectRoleRelationLogic)
        {
            _userLogic = userLogic;
            _cookieHelper = cookieHelper;
            _transformModel = transformModel;
            _userProjectRoleRelationLogic = userProjectRoleRelationLogic;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var userId = _cookieHelper.GetUserId(Request);
            _userProjectRoleRelationLogic.UserProjectRoleRelation = new UserProjectRoleRelation { UserId = userId };
            var userProjectRoleRelations= _userProjectRoleRelationLogic.GetByUserId();
            var indexViewModel = _transformModel.ToIndexViewModelFromUserProjectRoleRelations(userProjectRoleRelations);
            ViewBag.CurrentUserName = indexViewModel.UserName;
            return View(indexViewModel);
        }


        [HttpGet]
        public ActionResult Profiles()
        {
            try
            {
                var userId = _cookieHelper.GetUserId(Request);
                var user = _userLogic.Get(userId);
                ViewBag.CurrentUserName = user.UserName;
                var profileViewModel = _transformModel.ToProfileViewModelFromUser(user);
                return View(profileViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Profiles(ProfileViewModel profileViewModel)
        {
            try
            {
                var userModel = _transformModel.ToUserFromProfileViewModel(profileViewModel);
                userModel.UserId = _cookieHelper.GetUserId(Request);
                _userLogic.Update(userModel);
                return new RedirectResult(Url.Action("Index", "Users"));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult CreateProject()
        {
            var userId = _cookieHelper.GetUserId(Request);
            ViewBag.CurrentUserName = _userLogic.Get(userId).UserName;
            return View();
        }

        [HttpPost]
        public ActionResult CreateProject(CreateProjectViewModel createProjectViewModel)
        {
            try
            {
                var projectModel = _transformModel.ToProjectModelFromCreateProjectViewModel(createProjectViewModel);
                var userId = _cookieHelper.GetUserId(Request);
                _userLogic.CreateProject(userId, projectModel);
                return new RedirectResult(Url.Action("Index", "Users"));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}