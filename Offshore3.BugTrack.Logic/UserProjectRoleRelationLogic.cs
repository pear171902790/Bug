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
        private readonly ITransformModel _transformModel;
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public UserProjectRoleRelationLogic(IUserRepository userRepository, ITransformModel transformModel, IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _transformModel = transformModel;
            _userProjectRoleRelationRepository = userProjectRoleRelationRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
        }

        private UserProjectRoleRelation _userProjectRoleRelation;
        public UserProjectRoleRelation UserProjectRoleRelation
        {
            get { return _userProjectRoleRelation; }
            set {
                _userProjectRoleRelation = value;
                _userProjectRoleRelationRepository.UserProjectRoleRelation = _userProjectRoleRelation;
            }
        }

        public List<UserProjectRoleRelation> GetByUserId()
        {
           var userProjectRoleRelations =_userProjectRoleRelationRepository.GetByUserId();
            userProjectRoleRelations.ForEach(uprr =>
                {
                    .Project=new Project {ProjectId = uprr.ProjectId};
                    uprr.Project = _projectRepository.GetByProjectId();
                if (uprr.Project != null)
                {
                    uprr.Project.UserProjectRoleRelations = new List<UserProjectRoleRelation>();
                    _userProjectRoleRelationRepository.UserProjectRoleRelation=new UserProjectRoleRelation
                        {
                            ProjectId =uprr.ProjectId,
                            RoleId = RoleConfig.Creator
                        };
                    var userProjectRoleRelation = _userProjectRoleRelationRepository.GetByRoleIdAndProjectId();
                    _userRepository.User=new User{ UserId =userProjectRoleRelation.UserId };
                    userProjectRoleRelation.User = _userRepository.GetByUserId();
                    uprr.Project.UserProjectRoleRelations.Add(userProjectRoleRelation);
                }
            });
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
