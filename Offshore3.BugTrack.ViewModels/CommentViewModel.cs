using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public string Commentator { get; set; }
        public DateTime AddTime { get; set; }
    }
}
