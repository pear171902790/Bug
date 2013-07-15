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
      bool Add(User user);
      User GetByEmailAndPassword(string email,string password);
      User GetByUserNameAndPassword(string username,string password);
     
      void UpdateImageUrl(long userId,string imageUrl);
      void Update(User user);
      User GetByUserName();
  }
    
}
