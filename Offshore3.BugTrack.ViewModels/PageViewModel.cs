using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offshore3.BugTrack.ViewModels
{
    public class PageViewModel
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }

        public PageViewModel(int total,int count,int page)
        {
            Total = total;
            Count = count;
            Page = page;
            if (Total <= Count)
            {
                Pages = 1;
            }
            else
            {
                Pages = Total%Count != 0 ? Total/Count + 1 : Total/Count;
            }
        }
    }
}
