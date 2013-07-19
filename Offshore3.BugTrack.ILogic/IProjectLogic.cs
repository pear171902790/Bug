using System;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.ILogic
{
    public interface IProjectLogic
    {
        void Update(Project project);
        Project Get(long projectId);
        void CreateProject(Project project);
        Project Get(string projectName, Guid sole);
        void Delete(long projectId);
    }
}
