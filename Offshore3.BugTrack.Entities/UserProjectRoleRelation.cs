using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.Entities
{
    public class UserProjectRoleRelation
    {
        public long UserProjectRoleRelationId { get; set; }

        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public long RoleId { get; set; }

        public User User { get; set; }
        public Project Project { get; set; }
        public Role Role { get; set; }
    }
}
