using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.IRepository
{
    public interface IRepository<T>
    {
        T GetSingle(long id);
        List<T> GetAll();
        void Add(T userProjectRoleRelation);
        void Delete(long id);
    }
}
