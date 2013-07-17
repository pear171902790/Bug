using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();

        public Role Get(string roleName)
        {
            return _bugTrackDbContext.Roles.SingleOrDefault(r => r.RoleName == roleName);
        }

        public Role Get(long roleId)
        {
            return _bugTrackDbContext.Roles.SingleOrDefault(r => r.RoleId == roleId);
        }

    }
}
