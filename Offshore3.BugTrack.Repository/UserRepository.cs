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

        public User Get(long userId)
        {
            return _bugTrackDbContext.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public List<User> GetAll()
        {
            return _bugTrackDbContext.Users.ToList();
        }

        public bool Add(User user)
        {
            _bugTrackDbContext.Users.Add(user);
            return _bugTrackDbContext.SaveChanges()>0;
        }

        public void Delete(long id)
        {
            User user = Get(id);
            _bugTrackDbContext.Users.Remove(user);
        }

        public User GetByEmailAndPassword(string email,string password)
        {
            return _bugTrackDbContext.Users.SingleOrDefault(
                u => u.Email == email && u.Password == password
                );
        }

        public User GetByUserNameAndPassword(string username,string password)
        {
            return _bugTrackDbContext.Users.SingleOrDefault(
                u => u.Email == username && u.Password == password
                );
        }

        public void UpdateImageUrl(long userId, string imageUrl)
        {
            var user = Get(userId);
            user.ImageUrl = imageUrl;
            _bugTrackDbContext.SaveChanges();
        }

        public void Update(User user)
        {
            var single = Get(user.UserId);
            single.UserName = user.UserName;
            single.Password = user.Password;
            single.Gender = user.Gender;
            single.Introduction = user.Introduction;
            single.ImageUrl = user.ImageUrl;
            _bugTrackDbContext.SaveChanges();
        }

        public User GetUser(string userName)
        {
            return _bugTrackDbContext.Users.
                                      SingleOrDefault(u => u.UserName == userName);
        }
    }
}