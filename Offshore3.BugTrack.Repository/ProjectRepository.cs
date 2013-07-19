using System;
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

        public Project Get(long projectId)
        {
            return _bugTrackDbContext.Projects.SingleOrDefault(p => p.ProjectId == projectId);
        }

        public List<Project> GetAll()
        {
            return _bugTrackDbContext.Projects.ToList();
        }

        public void Add(Project project)
        {
            _bugTrackDbContext.Projects.Add(project);
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(long projectId)
        {
            var project = Get(projectId);
            _bugTrackDbContext.Projects.Remove(project);
            _bugTrackDbContext.SaveChanges();
        }

        public Project Get(string projectName, Guid sole)
        {
            return
                _bugTrackDbContext.Projects.SingleOrDefault(
                    p => p.ProjectName == projectName && p.Sole==sole);
        }

        public void Update(Project project)
        {
            var single = Get(project.ProjectId);
            single.ProjectName = project.ProjectName;
            single.Description = project.Description;
            _bugTrackDbContext.SaveChanges();
        }
    }
}