using System.Collections.Generic;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IUserProjectRoleRelationRepository 
    {
        UserProjectRoleRelation GetByRoleIdAndProjectId(long roleId,long projectId);
        UserProjectRoleRelation GetByRoleIdAndUserId(long roleId,long userId);
        List<UserProjectRoleRelation> GetByUserId(long userId);
        List<UserProjectRoleRelation> GetByProjectId(long projectId);
        void Add(UserProjectRoleRelation userProjectRoleRelation);
        UserProjectRoleRelation GetByUserIdAndProjectId(long userId, long projectId);
        void Update(UserProjectRoleRelation userProjectRoleRelation);
        void Delete(UserProjectRoleRelation userProjectRoleRelation);
    }
}
