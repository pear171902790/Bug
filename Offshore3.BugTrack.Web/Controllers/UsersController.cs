using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly IUserLogic _userLogic;
        private readonly IUserProjectRoleRelationLogic _userProjectRoleRelationLogic;
        private readonly IProjectLogic _projectLogic;



        public UsersController(IUserLogic userLogic, ICookieHelper cookieHelper,  IUserProjectRoleRelationLogic userProjectRoleRelationLogic,IProjectLogic projectLogic)
        {
            _userLogic = userLogic;
            _cookieHelper = cookieHelper;
            _userProjectRoleRelationLogic = userProjectRoleRelationLogic;
            _projectLogic = projectLogic;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var userId = _cookieHelper.GetUserId(Request);
            var userProjectRoleRelations= _userProjectRoleRelationLogic.GetByUserId(userId);
            var user = _userLogic.Get(userId);
            var projectViewModels=new List<ProjectViewModel>();
            userProjectRoleRelations.ForEach(uprr =>
                {
                    var project = _projectLogic.Get(uprr.ProjectId);
                    var creatorId = _userProjectRoleRelationLogic.GetByRoleIdAndProjectId(RoleConfig.Creator, uprr.ProjectId).UserId;
                    var creator = _userLogic.Get(creatorId);
                    var projectViewModel=new ProjectViewModel
                        {
                            ProjectId = project.ProjectId,
                            ProjectName = project.ProjectName,
                            Description = project.Description,
                            CreatDate = project.CreateDate,
                            CreatorName = creator.UserName,
                            RoleId = uprr.RoleId
                        };
                    projectViewModels.Add(projectViewModel);
                });
            var indexViewModel=new IndexViewModel
                {
                    CurrentUserName = user.UserName,
                    ProjectViewModels = projectViewModels
                };
            return View(indexViewModel);
        }


        [HttpGet]
        public ActionResult Profiles()
        {
            try
            {
                var userId = _cookieHelper.GetUserId(Request);
                var user = _userLogic.Get(userId);
                var profileViewModel =new ProfileViewModel
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.Password,
                        RepeatPassword = user.Password,
                        Gender = user.Gender,
                        Introduction = user.Introduction,
                        CurrentUserName = user.UserName,
                        ImageUrl =string.Format("{0}{1}.jpg", UserConfig.UserImageUrl,userId)
                    };
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
                var user = new User
                    {
                        UserName = profileViewModel.UserName,
                        Email = profileViewModel.Email,
                        Password = profileViewModel.Password,
                        Gender = profileViewModel.Gender,
                        Introduction = profileViewModel.Introduction,
                        UserId = _cookieHelper.GetUserId(Request),
                    };
                _userLogic.Update(user);

                if (profileViewModel.IsUpdateUserImage)
                {
                    var ioPath = Server.MapPath(UserConfig.UserImageUrl);
                    var imgPath = ioPath + user.UserId + ".jpg";
                    var tempImgPath = ioPath + user.UserId + "_temp.jpg";
                    System.IO.File.Delete(imgPath);
                    System.IO.File.Move(tempImgPath, imgPath);
                }

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