using System;
using System.Collections.Generic;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.Models;
using Offshore3.BugTrack.ViewModels;

namespace Offshore3.BugTrack.Web.Common
{
    public class TransformModel : ITransformModel
    {
        private readonly IRoleRelationLogic _roleRelationLogic;
        private readonly IRoleLogic _roleLogic;

        public TransformModel(IRoleRelationLogic roleRelationLogic,IRoleLogic roleLogic)
        {
            _roleRelationLogic = roleRelationLogic;
            _roleLogic = roleLogic;
        }

        public User ToUserFromLoginViewModel(LoginViewModel loginViewModel)
        {
            return new User
                {
                    Email = loginViewModel.Email,
                    Password = loginViewModel.Password
                };
        }

        public User ToUserFromRegisterViewModel(RegisterViewModel registerViewModel)
        {
            return new User
                {
                    UserName = registerViewModel.UserName,
                    Password = registerViewModel.Password,
                    Email = registerViewModel.Email
                };
        }

        public List<ProjectViewModel> ToProjectViewModelsFromRoleRelations(List<RoleRelation> roleRelations)
        {
            var projectViewModels=new List<ProjectViewModel>();
            roleRelations.ForEach(rr => projectViewModels.Add(ToProjectViewModelFromRoleRelation(rr)));
            return projectViewModels;
        }

        private ProjectViewModel ToProjectViewModelFromRoleRelation(RoleRelation roleRelation)
        {
            var role = _roleLogic.GetRole("Creator");
            return new ProjectViewModel()
                {
                    Project = roleRelation.Project,
                    Creator = _roleRelationLogic.GetRoleRelationByRoleIdAndProjectId(role.RoleId,roleRelation.ProjectId).User,
                    CurrentUserRole = roleRelation.Role
                };
        }

        public Project ToProjectFromCreateProjectViewModel(CreateProjectViewModel createProjectViewModel)
        {
            return new Project()
                {
                    ProjectName = createProjectViewModel.ProjectName,
                    Description = createProjectViewModel.Description,
                    CreateDate = DateTime.Now
                };
        }

        public ProfileViewModel ToProfileViewModelFromUser(User user)
        {
            var profileViewModel = new ProfileViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Gender = user.Gender,
                    ImageName = string.IsNullOrEmpty(user.ImageUrl) ? string.Empty : user.ImageUrl,
                    Introduction = user.Introduction
                };
            return profileViewModel;
        }

        public User ToUserFromProfileViewModel(ProfileViewModel profileViewModel)
        {
            var user = new User()
                {
                    UserName = profileViewModel.UserName,
                    Password = profileViewModel.Password,
                    Email = profileViewModel.Email,
                    Gender = profileViewModel.Gender,
                    ImageUrl = profileViewModel.ImageName,
                    Introduction = profileViewModel.Introduction
                };
            return user;

        }

        public SettingsViewModel ToSettingsViewModelFromProject(Project project)
        {
            return new SettingsViewModel()
                {
                    ProjectName = project.ProjectName,
                    Description = project.Description,
                    ProjectId = project.ProjectId
                };
        }

        public List<MemberViewModel> ToMemberViewModelsFromProjectMembers(List<ProjectMember> projectMembers)
        {
            var memberViewModels=new List<MemberViewModel>();
            projectMembers.ForEach(pm => memberViewModels.Add(ToMemberViewModelFromProjectMember(pm)));
            return memberViewModels;
        }

        public MemberViewModel ToMemberViewModelFromProjectMember(ProjectMember projectMember)
        {
            return new MemberViewModel()
                {
                    UserId = projectMember.UserId,
                    UserName = projectMember.UserName,
                    ImageName = projectMember.ImageUrl,
                    Introduction = projectMember.Introduction,
                    RoleName = projectMember.RoleName
                };
        }

        public Project ToProjectFromSettingsViewModel(SettingsViewModel settingsViewModel)
        {
            return new Project()
                {
                    ProjectId = settingsViewModel.ProjectId,
                    ProjectName = settingsViewModel.ProjectName,
                    Description = settingsViewModel.Description
                }; 
        }
    }
}