using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Offshore3.BugTrack.Entities
{
    public class User
    {


        public virtual long UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual bool Gender { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string Introduction { get; set; }

        public IUserLogic

        public List<UserProjectRoleRelation> RoleRelations { get; set; }
    }
}