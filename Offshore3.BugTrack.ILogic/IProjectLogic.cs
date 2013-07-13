using System.Collections.Generic;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.Models;

namespace Offshore3.BugTrack.ILogic
{
    public interface IProjectLogic
    {
        void Update(Project project);
        Project Get(long projectId);
    }
}
