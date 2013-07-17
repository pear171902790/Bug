using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class AddEditIssueViewModel
    {
        public IssueViewModel IssueViewModel { get; set; }
        public Dictionary<long, string> Members { get; set; }
        public List<string> BugStatusNames { get; set; }
        public long ProjectId { get; set; }
    }
   

    public class IssueViewModel
    {
        public long BugId { get; set; }
        public string BugName { get; set; }
        public string Description { get; set; }
        public string BugStatusName { get; set; }
        public long AssignerId { get; set; }
        public long ProjectId { get; set; }
    }
}
