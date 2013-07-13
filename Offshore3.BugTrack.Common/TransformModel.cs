using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.Models;
using Offshore3.BugTrack.ViewModels;

namespace Offshore3.BugTrack.Common
{
    public class TransformModel:ITransformModel
    {
        public User ToUserFromLoginViewModel(LoginViewModel loginViewModel)
        {
            return new User
                {
                    Email = loginViewModel.UserNameOrEmail,
                    Password = loginViewModel.Password,
                    UserName = loginViewModel.UserNameOrEmail
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

        public IndexViewModel ToIndexViewModelFromUserProjectRoleRelations(List<UserProjectRoleRelation> userProjectRoleRelations)
        {
            var userProjectViewModels=new List<UserProjectViewModel>();
            userProjectRoleRelations.ForEach(uprr =>
                {
                     var userProjectViewModel=new UserProjectViewModel
                        {
                            ProjectId = uprr.Project.ProjectId,
                            ProjectName = uprr.Project.ProjectName,
                            Description = uprr.Project.Description,
                            CreatDate = uprr.Project.CreateDate,
                            RoleId = uprr.Role.RoleId,
                            CreatorName = uprr.Project.UserProjectRoleRelations[0].
                        }
                }
                   
                );
        }
    }
}
