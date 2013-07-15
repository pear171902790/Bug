using System.Collections.Generic;
using Offshore3.BugTrack.Entities;


namespace Offshore3.BugTrack.ILogic
{
    public interface IUserLogic
    {
        bool AuthenticateUser(string email,string username,string password);
        User GetByUserNameAndPassword(string username,string password);
        User GetByEmailAndPassword(string email,string password);
        bool Register(User user);
        User Get(long userId);
        List<User> GetAll();
        void UpdateImageUrl(long userId,string imageUrl);
        void Update(User user);
    }
}
