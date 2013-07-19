using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.Entities
{
    public class Project
    {
        public virtual long ProjectId { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string Description { get; set; }
        public virtual Guid Sole { get; set; }

        public virtual List<BugStatus> BugStatuses { get; set; }
        public virtual List<UserProjectRoleRelation> UserProjectRoleRelations { get; set; }

    }
}
