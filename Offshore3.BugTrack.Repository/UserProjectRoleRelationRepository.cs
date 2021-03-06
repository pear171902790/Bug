﻿using System;
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

        public UserProjectRoleRelation GetByRoleIdAndUserId(long roleId, long userId)
        {
            return
                _bugTrackDbContext.UserProjectRoleRelations.SingleOrDefault(
                    uprr => uprr.RoleId == roleId && uprr.UserId == userId);
        }

        public List<UserProjectRoleRelation> GetByUserId(long userId)
        {
           return _bugTrackDbContext.UserProjectRoleRelations.Where(uprr => uprr.UserId == userId).ToList();
        }

        public List<UserProjectRoleRelation> GetByProjectId(long projectId)
        {
            return _bugTrackDbContext.UserProjectRoleRelations.Where(uprr => uprr.ProjectId == projectId).ToList();
        }

        public void Add(UserProjectRoleRelation userProjectRoleRelation)
        {
            _bugTrackDbContext.UserProjectRoleRelations.Add(userProjectRoleRelation);
            _bugTrackDbContext.SaveChanges();
        }

        public UserProjectRoleRelation GetByUserIdAndProjectId(long userId, long projectId)
        {
            return
                _bugTrackDbContext.UserProjectRoleRelations.SingleOrDefault(
                    uprr => uprr.UserId == userId && uprr.ProjectId == projectId);
        }

        public void Update(UserProjectRoleRelation userProjectRoleRelation)
        {
            var single = GetByUserIdAndProjectId(userProjectRoleRelation.UserId, userProjectRoleRelation.ProjectId);
            single.RoleId = userProjectRoleRelation.RoleId;
            _bugTrackDbContext.SaveChanges();
        }

        public void Delete(UserProjectRoleRelation userProjectRoleRelation)
        {
            var single = GetByUserIdAndProjectId(userProjectRoleRelation.UserId, userProjectRoleRelation.ProjectId);
            _bugTrackDbContext.UserProjectRoleRelations.Remove(single);
        }

        public UserProjectRoleRelation GetByRoleIdAndProjectId(long roleId,long projectId)
        {
            return
                _bugTrackDbContext.UserProjectRoleRelations.SingleOrDefault(
                    uprr => uprr.RoleId == roleId && uprr.ProjectId == projectId);
        }
    }
}
