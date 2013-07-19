using System;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic
{
    public class ProjectLogic:IProjectLogic
    {
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public ProjectLogic(IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
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

        public Project Get(string projectName, Guid sole)
        {
            return _projectRepository.Get(projectName, sole);
        }

        public void Delete(long projectId)
        {
            _projectRepository.Delete(projectId);
        }

        public Project Get(long projectId)
        {
           return _projectRepository.Get(projectId);
        }

        

        

      

       

       
    }
}
