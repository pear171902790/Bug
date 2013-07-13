using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IProjectRepository:IRepository<Project>
    {
        Project Project { get; set; }

        Project GetProject(string projectName);
        void Update(Project project);
        Project GetByProjectId();
    }
}
