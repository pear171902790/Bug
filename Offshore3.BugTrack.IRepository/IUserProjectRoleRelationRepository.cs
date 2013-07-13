using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IUserProjectRoleRelationRepository : IRepository<UserProjectRoleRelation>
    {
        UserProjectRoleRelation UserProjectRoleRelation { get; set; }

        UserProjectRoleRelation GetByRoleIdAndProjectId();
        UserProjectRoleRelation GetByRoleIdAndUserId();
        List<UserProjectRoleRelation> GetByUserId();
        List<UserProjectRoleRelation> GetByProjectId();
        UserProjectRoleRelation GetRoleRelationByRoleIdAndProjectId(long roleId, long projectId);
        UserProjectRoleRelation GetRoleRelationByUserIdAndProjectId(long userId, long projectId);
        void UpdateRoleRelation(UserProjectRoleRelation userProjectRoleRelation);
    }
}
