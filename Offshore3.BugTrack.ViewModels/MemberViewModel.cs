using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class MemberViewModel
    {
        public long UserId { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Introduction { get; set; }
        public bool Gender { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }
    
}
