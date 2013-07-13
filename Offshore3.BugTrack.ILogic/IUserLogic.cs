using System.Collections.Generic;
using Offshore3.BugTrack.Entities;


namespace Offshore3.BugTrack.ILogic
{
    public interface IUserLogic
    {
        User User { get; set; }


        bool AuthenticateUser();
        User GetByUserNameAndPassword();
        User GetByEmailAndPassword();
        bool Register();
        User Get();
        List<User> GetAll();
        void UpdateImageUrl();
        void Update();
    }
}
