using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
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
        private readonly IUserProjectRoleRelationLogic _userProjectRoleRelationLogic;
        private readonly IRoleLogic _roleLogic;

        public ProjectController
            (
                IUserLogic userLogic,
                IProjectLogic projectLogic, 
                ICookieHelper cookieHelper,
                IUserProjectRoleRelationLogic userProjectRoleRelationLogic,
                IRoleLogic roleLogic
            )
        {
            _userLogic = userLogic;
            _projectLogic = projectLogic;
            _cookieHelper = cookieHelper;
            _userProjectRoleRelationLogic = userProjectRoleRelationLogic;
            _roleLogic = roleLogic;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var userId = _cookieHelper.GetUserId(Request);
            var userProjectRoleRelations = _userProjectRoleRelationLogic.GetByUserId(userId);
            var user = _userLogic.Get(userId);
            var projectViewModels = new List<ProjectViewModel>();
            userProjectRoleRelations.ForEach(uprr =>
            {
                var project = _projectLogic.Get(uprr.ProjectId);
                var creatorId = _userProjectRoleRelationLogic.GetByRoleIdAndProjectId(RoleConfig.Creator, uprr.ProjectId).UserId;
                var creator = _userLogic.Get(creatorId);
                var projectViewModel = new ProjectViewModel
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
            var indexViewModel = new IndexViewModel
            {
                CurrentUserName = user.UserName,
                ProjectViewModels = projectViewModels
            };
            return View(indexViewModel);
        }

        [HttpGet]
        public ActionResult CreateProject()
        {
            var userId = _cookieHelper.GetUserId(Request);
            var createProjectViewModel = new CreateProjectViewModel
            {
                CurrentUserName = _userLogic.Get(userId).UserName
            };
            return View(createProjectViewModel);
        }

        [HttpPost]
        public ActionResult CreateProject(CreateProjectViewModel createProjectViewModel)
        {
            try
            {
                var project = new Project
                {
                    ProjectName = createProjectViewModel.ProjectName,
                    Description = createProjectViewModel.Description,
                    CreateDate = DateTime.Now,
                    Sole = Guid.NewGuid()
                };
                var userId = _cookieHelper.GetUserId(Request);
                _projectLogic.CreateProject(project);
                var projectId = _projectLogic.Get(project.ProjectName,project.Sole).ProjectId;
                var userProjectRoleRelation=new UserProjectRoleRelation
                    {
                        ProjectId = projectId,
                        UserId = userId,
                        RoleId = RoleConfig.Creator
                    };
                _userProjectRoleRelationLogic.Add(userProjectRoleRelation);
                return new RedirectResult(Url.Action("Index", "Project"));
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult MemberList(long projectId)
        {
            var currentUserId = _cookieHelper.GetUserId(Request);
            var currentUserRoleId = _userProjectRoleRelationLogic.GetByUserIdAndProjectId(currentUserId, projectId).RoleId;
            var userProjectRoleRelation = _userProjectRoleRelationLogic.GetByProjectId(projectId);
            var memberViewModels=new List<MemberViewModel>();
            userProjectRoleRelation.ForEach(uprr =>
                {
                    var user = _userLogic.Get(uprr.UserId);
                    var role = _roleLogic.Get(uprr.RoleId);
                    var webPath =UserConfig.UserImageUrl;
                    var memberViewModel = new MemberViewModel
                        {
                            UserId = user.UserId,
                            ImageUrl = webPath + user.UserId + ".jpg",
                            UserName = user.UserName,
                            Introduction = user.Introduction,
                            RoleId = role.RoleId,
                            RoleName = role.RoleName
                        };
                    memberViewModels.Add(memberViewModel);
                });
            var memberListViewModel=new MemberListViewModel()
                {
                    ProjectId = projectId,
                    CurrentUserRoleId = currentUserRoleId,
                    MemberViewModels = memberViewModels
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
            var detailsViewModel=new DetailsViewModel
                {
                    CurrentUserName = _userLogic.Get(userId).UserName,
                    ProjectId = id
                };
            return View(detailsViewModel);
        }

        public ActionResult Settings(long projectId)
        {
            var project = _projectLogic.Get(projectId);
            var settingsViewModel = new SettingsViewModel
                {
                    ProjectName = project.ProjectName,
                    Description = project.Description,
                    ProjectId = project.ProjectId
                };
            return PartialView(settingsViewModel);
        }
        
        public ActionResult Update([ModelBinder(typeof(JsonBinder<SettingsViewModel>))]SettingsViewModel settingsViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                var project = new Project
                    {
                        ProjectId = settingsViewModel.ProjectId,
                        ProjectName = settingsViewModel.ProjectName,
                        Description = settingsViewModel.Description
                    };
                _projectLogic.Update(project);

                jsonResult.Data = new { IsSuccess = true };
            }
            catch (Exception)
            {
                jsonResult.Data = new { IsSuccess = false };
            }
            return jsonResult;
        }

        public ActionResult UpdateRelation(MemberListViewModel memberListViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                var userProjectRoleRelation=new UserProjectRoleRelation
                    {
                        UserId = memberListViewModel.UserId,
                        ProjectId = memberListViewModel.ProjectId,
                        RoleId = memberListViewModel.RoleId
                    };
                _userProjectRoleRelationLogic.Update(userProjectRoleRelation);
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
                var user = _userLogic.GetByUserName(memberManagerViewModel.UserName);
                if (user == null)
                {
                    user=new User
                        {
                            UserName = memberManagerViewModel.UserName,
                            Password = UserConfig.InitialPassword,
                            Email = memberManagerViewModel.UserName+UserConfig.InitialEmailExt
                        };
                    _userLogic.Register(user);
                    user = _userLogic.GetByUserName(user.UserName);
                }
                if (_userProjectRoleRelationLogic.GetByUserIdAndProjectId(user.UserId, memberManagerViewModel.ProjectId)==null)
                {
                    var userProjectRoleRelation = new UserProjectRoleRelation
                        {
                            UserId = user.UserId,
                            ProjectId = memberManagerViewModel.ProjectId,
                            RoleId = memberManagerViewModel.RoleId
                        };
                    _userProjectRoleRelationLogic.Add(userProjectRoleRelation);
                }
                jsonResult.Data = new { IsSuccess = true };
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
                var userProjectRoleRelation = new UserProjectRoleRelation
                {
                    UserId = memberListViewModel.UserId,
                    ProjectId = memberListViewModel.ProjectId,
                    RoleId = memberListViewModel.RoleId
                };
                _userProjectRoleRelationLogic.Delete(userProjectRoleRelation);
                jsonResult.Data = new { IsSuccess = true };

            }
            catch (Exception)
            {
                jsonResult.Data = new { IsSuccess = false };
            }
            return jsonResult;
        }

        public ActionResult Delete(long projectId)
        {
            var jsonResult = new JsonResult();
            try
            {
                _projectLogic.Delete(projectId);
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