using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Logic
{
    public class RoleLogic :IRoleLogic
    {
        private readonly IRoleRepository _roleRepository;

        public RoleLogic(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role GetRole(string roleName)
        {
            return _roleRepository.GetRole(roleName);
        }
    }
}
