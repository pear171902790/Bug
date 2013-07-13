using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offshore3.BugTrack.Entities;

namespace Offshore3.BugTrack.IRepository
{
  public interface IUserRepository:IRepository<User>
  {
      User User { get; set; }

      User GetByUserId();
      bool Add();
      User GetByEmailAndPassword();
      User GetByUserNameAndPassword();
     
      void UpdateImageUrl();
      void Update();
      User GetByUserName();
  }
    
}
