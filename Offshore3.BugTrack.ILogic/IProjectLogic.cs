using System;
using System.Collections.Generic;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.Models;

namespace Offshore3.BugTrack.ILogic
{
    public interface IProjectLogic
    {
        void Update(Project project);
        Project Get(long projectId);
        void CreateProject(Project project);
        Project Get(string projectName,DateTime createDate);
        void Delete(long projectId);
    }
}
