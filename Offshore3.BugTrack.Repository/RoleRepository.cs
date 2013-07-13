using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class RoleRepository:IRoleRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();

        private Role Get(long id)
        {
            return _bugTrackDbContext.Roles.SingleOrDefault(r => r.RoleId == id);
        }

        public Role GetSingle(long id)
        {
            return Get(id);
        }

        public List<Role> GetAll()
        {
            return _bugTrackDbContext.Roles.ToList();
        }

        public void Add(Role userProjectRoleRelation)
        {
            _bugTrackDbContext.Roles.Add(userProjectRoleRelation);
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var role = Get(id);
            _bugTrackDbContext.Roles.Remove(role);
            _bugTrackDbContext.SaveChanges();
        }

        public Role GetRole(string roleName)
        {
            return _bugTrackDbContext.Roles.SingleOrDefault(r => r.RoleName == roleName);
        }
    }
}
