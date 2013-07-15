using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic
{
    public class UserProjectRoleRelationLogic : IUserProjectRoleRelationLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public UserProjectRoleRelationLogic(IUserRepository userRepository,  IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _userProjectRoleRelationRepository = userProjectRoleRelationRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
        }

        public List<UserProjectRoleRelation> GetByUserId(long userId)
        {
           return _userProjectRoleRelationRepository.GetByUserId(userId);
        }

        public UserProjectRoleRelation GetByRoleIdAndProjectId(long roleId, long projectId)
        {
            return _userProjectRoleRelationRepository.GetByRoleIdAndProjectId(roleId, projectId);
        }

        public bool CheckIsMember(long projectId, long userId)
        {
            throw new NotImplementedException();
        }

        public void AddMember(long userId, long projectId, long roleId)
        {
            throw new NotImplementedException();
        }

        public long GetRoleId(long projectId, long userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleRelation(long projectId, long userId, long roleId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(long projectId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
