using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;
using Offshore3.BugTrack.Models;
using Offshore3.BugTrack.ViewModels;

namespace Offshore3.BugTrack.Logic
{
    public class ProjectLogic:IProjectLogic
    {
        private readonly ITransformModel _transformModel;
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public ProjectLogic(ITransformModel transformModel, IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
            _transformModel = transformModel;
            _userProjectRoleRelationRepository = userProjectRoleRelationRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
        }

        public ProjectModel GetProjectModelWithMembers(long projectId)
        {
            var project=_projectRepository.GetSingle(projectId);
            var projectModel = _transformModel.ToProjectModelFromProject(project);

            var roleRelations = _userProjectRoleRelationRepository.GetRoleRelationsByProjectId(projectId);
            var memebers = new List<UserModel>();
            roleRelations.ForEach(rr =>
            {
                var userModel = _transformModel.ToUserModelFromUser(rr.User);
                userModel.CurrentProjectRoleId = rr.Role.RoleId;
                userModel.CurrentProjectRoleName = rr.Role.RoleName;
                memebers.Add(userModel);
            });
            projectModel.Members = memebers;
            return projectModel;
        }

        public void Update(ProjectModel projectModel)
        {
            var project = _transformModel.ToProjectFromProjectModel(projectModel);
            _projectRepository.Update(project);
        }

        public ProjectModel Get(long projectId)
        {
            var project = _projectRepository.GetSingle(projectId);
            return _transformModel.ToProjectModelFromProject(project);
        }

        public bool CheckIsMember(long projectId, long userId)
        {
            var roleRelation=_userProjectRoleRelationRepository.GetRoleRelationByUserIdAndProjectId(userId,projectId);
            if (roleRelation!=null)
            {
                return true;
            }
            return false;
        }

        public void AddMember(long userId, long projectId, long roleId)
        {
            var roleRelation=new RoleRelation()
                {
                    UserId = userId,
                    ProjectId = projectId,
                    RoleId = roleId
                };
            _userProjectRoleRelationRepository.Add(roleRelation);
        }

        public long GetRoleId(long projectId, long userId)
        {
            return _userProjectRoleRelationRepository.GetRoleRelationByUserIdAndProjectId(userId, projectId).RoleId;
        }

        public void UpdateRoleRelation(long projectId, long userId,long roleId)
        {
            var roleRelation = new RoleRelation()
                {
                    RoleId = roleId,
                    UserId = userId,
                    ProjectId = projectId
                };
            _userProjectRoleRelationRepository.UpdateRoleRelation(roleRelation);
        }

        public void DeleteMember(long projectId, long userId)
        {
            var roleRelationId=_userProjectRoleRelationRepository.GetRoleRelationByUserIdAndProjectId(userId, projectId).RoleRelationId;
            _userProjectRoleRelationRepository.Delete(roleRelationId);
        }
    }
}
