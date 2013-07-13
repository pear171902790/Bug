using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.Entities
{
    public class BugTrackDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<BugStatus> BugStatuses { get; set; }
        public DbSet<UserProjectRoleRelation> UserProjectRoleRelations { get; set; }
        public DbSet<BugComment> BugComments { get; set; }
    }

    

    

    

    



    
}
