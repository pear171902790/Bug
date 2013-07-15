using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IUserProjectRoleRelationRepository 
    {
        UserProjectRoleRelation GetByRoleIdAndProjectId(long roleId,long projectId);
        UserProjectRoleRelation GetByRoleIdAndUserId(long roleId,long userId);
        List<UserProjectRoleRelation> GetByUserId(long userId);
        List<UserProjectRoleRelation> GetByProjectId(long projectId);
    }
}
