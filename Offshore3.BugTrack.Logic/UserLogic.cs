using System;
using System.Collections.Generic;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;


namespace Offshore3.BugTrack.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransformModel _transformModel;
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public UserLogic(IUserRepository userRepository, ITransformModel transformModel, IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _transformModel = transformModel;
            _userProjectRoleRelationRepository = userProjectRoleRelationRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
            
        }

        private User _user;
        public User User {
            get { return _user; }
            set { 
                _user = value;
                _userRepository.User = _user;
            }
        }

        public bool Register()
        {
            return _userRepository.Add();
        }

        public bool AuthenticateUser()
        {
            return (_userRepository.GetByEmailAndPassword() != null ||
                    _userRepository.GetByUserNameAndPassword() != null);
        }

        public UserModel Get(long userId)
        {
            var user=_userRepository.GetSingle(userId);
            return _transformModel.ToUserModelFromUser(user);
        }

        public User GetByEmailAndPassword()
        {
            return _userRepository.GetByEmailAndPassword();
        }

        public User GetByUserNameAndPassword()
        {
            return _userRepository.GetByUserNameAndPassword();
        }

        public List<UserModel> GetAll()
        {
            var users= _userRepository.GetAll();
            return _transformModel.ToUserModelsFromUsers(users);
        }

        public void UpdateImageUrl(long userId, string imageUrl)
        {
            _userRepository.UpdateImageUrl(userId, imageUrl);
        }

        public void Update(UserModel userModel)
        {
            var user=_transformModel.ToUserFromUserModel(userModel);
            _userRepository.Update(user);
        }

        public UserModel Get(string userName)
        {
            var user= _userRepository.GetUser(userName);
            if (user == null)
                return null;
            return _transformModel.ToUserModelFromUser(user);
        }

        public UserModel GetUserModelWithProjects(long userId)
        {
            var user = _userRepository.GetSingle(userId);
            var userModel = _transformModel.ToUserModelFromUser(user);
            var roleRelations = _userProjectRoleRelationRepository.GetRoleRelationsByUserId(userId);
            var projectModels = new List<ProjectModel>();
            roleRelations.ForEach(rr =>
                {
                    var project = rr.Project;
                    var projectModel = _transformModel.ToProjectModelFromProject(project);
                    projectModel.CurrentUserRoleId = rr.Role.RoleId;
                    projectModel.CurrentUserRoleName = rr.Role.RoleName;
                    var roleId = RoleConfig.Creator;
                    var creatorId = _userProjectRoleRelationRepository
                        .GetRoleRelationByRoleIdAndProjectId(roleId,project.ProjectId).UserId;
                    var creator = _userRepository.GetSingle(creatorId);
                    projectModel.Creator = _transformModel.ToUserModelFromUser(creator);
                    projectModels.Add(projectModel);
                });
            userModel.Projects = projectModels;
            return userModel;
        }

        public void CreateProject(long userId, ProjectModel projectModel)
        {
            projectModel.CreateDate = DateTime.Now;
            var project = _transformModel.ToProjectFromProjectModel(projectModel);
            _projectRepository.Add(project);
            var projectId = _projectRepository.GetProject(project.ProjectName).ProjectId;
            var roleId = RoleConfig.Creator;
            var roleRelation = new RoleRelation()
                {
                    UserId = userId,
                    ProjectId = projectId,
                    RoleId = roleId
                };
            _userProjectRoleRelationRepository.Add(roleRelation);
        }
    }
}