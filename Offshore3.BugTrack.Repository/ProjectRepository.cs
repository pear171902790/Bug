using System.Collections.Generic;
using System.Linq;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext;

        public Project Project { get; set; }


        public ProjectRepository()
        {
            _bugTrackDbContext = new BugTrackDbContext();
        }

        public Project GetByProjectId()
        {
            return _bugTrackDbContext.Projects.SingleOrDefault(p => p.ProjectId == Project.ProjectId);
        }

        public List<Project> GetAll()
        {
            return _bugTrackDbContext.Projects.ToList();
        }

        public void Add(Project userProjectRoleRelation)
        {
            _bugTrackDbContext.Projects.Add(userProjectRoleRelation);
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            Project project = GetSingle(id);
            _bugTrackDbContext.Projects.Remove(project);
            _bugTrackDbContext.SaveChanges();
        }

        public Project GetProject(string projectName)
        {
            return
                _bugTrackDbContext.Projects.SingleOrDefault(
                    p => p.ProjectName == projectName);
        }

        public void Update(Project project)
        {
            var single = GetSingle(project.ProjectId);
            single.ProjectName = project.ProjectName;
            single.Description = project.Description;
            _bugTrackDbContext.SaveChanges();
        }
    }
}