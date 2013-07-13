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
        UserProjectRoleRelation UserProjectRoleRelation { get; set; }

        List<UserProjectRoleRelation> GetByUserId();
        bool CheckIsMember(long projectId, long userId);
        void AddMember(long userId, long projectId, long roleId);
        long GetRoleId(long projectId, long userId);
        void UpdateRoleRelation(long projectId, long userId, long roleId);
        void DeleteMember(long projectId, long userId);
    }
}
