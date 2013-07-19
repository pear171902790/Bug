using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
    public interface IProjectRepository
    {
        void Delete(long projectId);
        Project Get(string projectName,Guid sole);
        void Update(Project project);
        Project Get(long projectId);
        void Add(Project project);


    }
}
