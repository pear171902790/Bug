using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.Models;
using Offshore3.BugTrack.ViewModels;

namespace Offshore3.BugTrack.Web.Common
{
    public interface ITransformModel
    {
        User ToUserFromLoginViewModel(LoginViewModel loginViewModel);
        User ToUserFromRegisterViewModel(RegisterViewModel registerViewModel);

        List<ProjectViewModel> ToProjectViewModelsFromRoleRelations(List<RoleRelation> roleRelations);

        Project ToProjectFromCreateProjectViewModel(CreateProjectViewModel createProjectViewModel);
        ProfileViewModel ToProfileViewModelFromUser(User user);
        User ToUserFromProfileViewModel(ProfileViewModel profileViewModel);
        SettingsViewModel ToSettingsViewModelFromProject(Project project);
        List<MemberViewModel> ToMemberViewModelsFromProjectMembers(List<ProjectMember> projectMembers);
        MemberViewModel ToMemberViewModelFromProjectMember(ProjectMember projectMember);
        Project ToProjectFromSettingsViewModel(SettingsViewModel settingsViewModel);
    }
}
