using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.Entities
{
    public class Role
    {
        public virtual long RoleId { get; set; }
        public virtual string RoleName { get; set; }

        public List<UserProjectRoleRelation> UserProjectRoleRelations { get; set; }
    }
}
        