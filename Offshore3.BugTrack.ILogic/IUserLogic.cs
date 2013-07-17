using System.Collections.Generic;
using Offshore3.BugTrack.Entities;


namespace Offshore3.BugTrack.ILogic
{
    public interface IUserLogic
    {
        bool AuthenticateUser(User user);
        User GetByUserNameAndPassword(string username,string password);
        User GetByEmailAndPassword(string email,string password);
        bool Register(User user);
        User Get(long userId);
        User GetByEmail(string email);
        void Update(User user);
        User GetByUserName(string userName);
    }
}
