using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class UserProjectRoleRelationRepository : IUserProjectRoleRelationRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();

        public UserProjectRoleRelation UserProjectRoleRelation { get; set; }

        public UserProjectRoleRelation GetSingle(long id)
        {
            return _bugTrackDbContext.RoleRelations.SingleOrDefault(rr => rr.RoleRelationId == id);
        }

        public List<UserProjectRoleRelation> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(UserProjectRoleRelation userProjectRoleRelation)
        {
            _bugTrackDbContext.RoleRelations.Add(userProjectRoleRelation);
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var roleRelation = GetSingle(id);
            _bugTrackDbContext.RoleRelations.Remove(roleRelation);
            _bugTrackDbContext.SaveChanges();
        }

        public List<UserProjectRoleRelation> GetByUserId()
        {
            var userId = UserProjectRoleRelation.UserId;
           return _bugTrackDbContext.UserProjectRoleRelations.Where(uprr => uprr.UserId == userId).ToList();
            
        }

        public UserProjectRoleRelation GetByRoleIdAndProjectId()
        {

        }

        public List<UserProjectRoleRelation> GetRoleRelationsByProjectId(long projectId)
        {
            var roleRelations= _bugTrackDbContext.RoleRelations.Where(rr => rr.ProjectId == projectId).ToList();
            roleRelations.ForEach(rr =>
                {
                    rr.User = _bugTrackDbContext.Users.SingleOrDefault(u => u.UserId == rr.UserId);
                    rr.Project = _bugTrackDbContext.Projects.SingleOrDefault(p => p.ProjectId == rr.ProjectId);
                    rr.Role = _bugTrackDbContext.Roles.SingleOrDefault(r => r.RoleId == rr.RoleId);
                });
            return roleRelations;
        }

        public UserProjectRoleRelation GetRoleRelationByRoleIdAndProjectId(long roleId, long projectId)
        {
            var roleRelation=
                _bugTrackDbContext.RoleRelations.FirstOrDefault(rr => rr.RoleId == roleId && rr.ProjectId == projectId);
            roleRelation.User = _bugTrackDbContext.Users.SingleOrDefault(u => u.UserId == roleRelation.UserId);
            roleRelation.Project = _bugTrackDbContext.Projects.SingleOrDefault(p => p.ProjectId == roleRelation.ProjectId);
            roleRelation.Role = _bugTrackDbContext.Roles.SingleOrDefault(r => r.RoleId == roleRelation.RoleId);
            return roleRelation;
        }

        public UserProjectRoleRelation GetRoleRelationByUserIdAndProjectId(long userId, long projectId)
        {
            return
                _bugTrackDbContext.RoleRelations.SingleOrDefault(rr => rr.UserId == userId && rr.ProjectId == projectId);
        }

        public void UpdateRoleRelation(UserProjectRoleRelation userProjectRoleRelation)
        {
            var single = GetRoleRelationByUserIdAndProjectId(userProjectRoleRelation.UserId, userProjectRoleRelation.ProjectId);
            single.RoleId = userProjectRoleRelation.RoleId;
            _bugTrackDbContext.SaveChanges();
        }
    }
}
