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
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public UserLogic(IUserRepository userRepository, IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _userProjectRoleRelationRepository = userProjectRoleRelationRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
            
        }

        public bool Register(User user)
        {
            return _userRepository.Add(user);
        }

        public bool AuthenticateUser(string email,string username,string password)
        {
            return (_userRepository.GetByEmailAndPassword(email,password) != null ||
                    _userRepository.GetByUserNameAndPassword(username,password) != null);
        }

        public User Get(long userId)
        {
            return _userRepository.Get(userId);
        }

        public User GetByEmailAndPassword(string email,string password)
        {
            return _userRepository.GetByEmailAndPassword(email,password);
        }

        public User GetByUserNameAndPassword(string username,string password)
        {
            return _userRepository.GetByUserNameAndPassword(username,password);
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

        public void Update(User user)
        {
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