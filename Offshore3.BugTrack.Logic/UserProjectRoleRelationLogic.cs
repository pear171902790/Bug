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

        public List<UserProjectRoleRelation> GetByProjectId(long projectId)
        {
            return _userProjectRoleRelationRepository.GetByProjectId(projectId);
        }

        public void Delete(UserProjectRoleRelation userProjectRoleRelation)
        {
            _userProjectRoleRelationRepository.Delete(userProjectRoleRelation);
        }

        public UserProjectRoleRelation GetByRoleIdAndProjectId(long roleId, long projectId)
        {
            return _userProjectRoleRelationRepository.GetByRoleIdAndProjectId(roleId, projectId);
        }

        public UserProjectRoleRelation GetByUserIdAndProjectId(long userId, long projectId)
        {
            return _userProjectRoleRelationRepository.GetByUserIdAndProjectId(userId, projectId);
        }

        public void Add(UserProjectRoleRelation userProjectRoleRelation)
        {
            _userProjectRoleRelationRepository.Add(userProjectRoleRelation);
        }

        public void Update(UserProjectRoleRelation userProjectRoleRelation)
        {
            _userProjectRoleRelationRepository.Update(userProjectRoleRelation);
        }

      

       
    }
}
