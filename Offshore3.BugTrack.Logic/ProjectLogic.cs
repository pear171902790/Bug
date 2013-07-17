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
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public ProjectLogic(IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
            _transformModel = transformModel;
            _userProjectRoleRelationRepository = userProjectRoleRelationRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
        }

        public void Update(Project project)
        {
            _projectRepository.Update(project);
        }

        

        public void CreateProject(Project project)
        {
            _projectRepository.Add(project);
        }

        public Project Get(string projectName, DateTime createDate)
        {
            return _projectRepository.Get(projectName,createDate);
        }

        public void Delete(long projectId)
        {
            _projectRepository.Delete(projectId);
        }

        public Project Get(long projectId)
        {
           return _projectRepository.Get(projectId);
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
