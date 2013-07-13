using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class MemberListViewModel
    {
        public long CurrentUserRoleId { get; set; }
        public long ProjectId { get; set; }
        public List<MemberViewModel> MemberViewModels { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
