using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic
{
    public class RoleRelationLogic : IRoleRelationLogic
    {
        private readonly IRoleRelationRepository _roleRelationRepository;

        public RoleRelationLogic(IRoleRelationRepository roleRelationRepository)
        {
            _roleRelationRepository = roleRelationRepository;
        }

        public List<RoleRelation> GetRoleRelationsByUserId(long userId)
        {
            return _roleRelationRepository.GetRoleRelationsByUserId(userId);
        }

        public List<RoleRelation> GetRoleRelationsByProjectId(long projectId)
        {
            return _roleRelationRepository.GetRoleRelationsByProjectId(projectId);
        }

        public RoleRelation GetRoleRelationByRoleIdAndProjectId(long roleId, long projectId)
        {
            return _roleRelationRepository.GetRoleRelationByRoleIdAndProjectId(roleId, projectId);
        }
    }
}
