using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class BugAttachmentsViewModel
    {
        public List<string> AttachmentNames { get; set; }
        public long BugId { get; set; }
    }
}
