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
    public interface ITransformModel
    {
        User ToUserFromLoginViewModel(LoginViewModel loginViewModel);
        User ToUserFromRegisterViewModel(RegisterViewModel registerViewModel);
        IndexViewModel ToIndexViewModelFromUserProjectRoleRelations(List<UserProjectRoleRelation> userProjectRoleRelations);
    }
}
