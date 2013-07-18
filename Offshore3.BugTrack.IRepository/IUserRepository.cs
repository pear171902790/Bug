using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
  public interface IUserRepository
  {
      User Get(long userId);
      User GetByUserName(string userName);
      User GetByEmail(string email);
      bool Add(User user);
      User GetByEmailAndPassword(string email,string password);
      User GetByUserNameAndPassword(string username,string password);
     
      void Update(User user);
      
  }
    
}
