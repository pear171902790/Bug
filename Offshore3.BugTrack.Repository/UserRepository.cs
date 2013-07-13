using System;
using System.Collections.Generic;
using System.Linq;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.IRepository;

namespace Offshore3.BugTrack.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BugTrackDbContext _bugTrackDbContext = new BugTrackDbContext();


        public User User { get; set; }


        public User GetByUserId()
        {
            return _bugTrackDbContext.Users.SingleOrDefault(u => u.UserId == User.UserId);
        }

        public List<User> GetAll()
        {
            return _bugTrackDbContext.Users.ToList();
        }

        public bool Add()
        {
            _bugTrackDbContext.Users.Add(User);
            return _bugTrackDbContext.SaveChanges()>0;
        }

        public void Delete(long id)
        {
            User user = Get(id);
            _bugTrackDbContext.Users.Remove(user);
        }

        public User GetByEmailAndPassword()
        {
            return _bugTrackDbContext.Users.SingleOrDefault(
                u => u.Email == User.Email && u.Password == User.Password
                );
        }

        public User GetByUserNameAndPassword()
        {
            return _bugTrackDbContext.Users.SingleOrDefault(
                u => u.Email == User.UserName && u.Password == User.Password
                );
        }

        public List<UserProjectRoleRelation> GetRoleRelations(long userId)
        {
            return _bugTrackDbContext.RoleRelations.Where(rr => rr.UserId == userId).ToList();
        }

        public void UpdateImageUrl(long userId, string imageUrl)
        {
            var user = Get(userId);
            user.ImageUrl = imageUrl;
            _bugTrackDbContext.SaveChanges();
        }

        public void Update(User user)
        {
            User single = Get(user.UserId);
            single.UserName = user.UserName;
            single.Password = user.Password;
            single.Gender = user.Gender;
            single.Introduction = user.Introduction;
            _bugTrackDbContext.SaveChanges();
        }

        public User GetUser(string userName)
        {
            return _bugTrackDbContext.Users.
                                      SingleOrDefault(u => u.UserName == userName);
        }
    }
}