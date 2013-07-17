using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ILogic
{
    public interface IUserProjectRoleRelationLogic
    {
        List<UserProjectRoleRelation> GetByUserId(long userId);
        List<UserProjectRoleRelation>  GetByProjectId(long projectId);
        void Delete(UserProjectRoleRelation userProjectRoleRelation);
        UserProjectRoleRelation GetByRoleIdAndProjectId(long roleId, long projectId);
        UserProjectRoleRelation GetByUserIdAndProjectId(long userId, long projectId);
        void Add(UserProjectRoleRelation userProjectRoleRelation);
        void Update(UserProjectRoleRelation userProjectRoleRelation);
    }
}
