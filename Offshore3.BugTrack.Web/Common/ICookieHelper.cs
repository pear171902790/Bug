using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Offshore3.BugTrack.Web.Common
{
    public interface ICookieHelper
    {
        void SetAuthCookie(string id, bool isRememberMe);
        long GetUserId(HttpRequestBase request);
        void SignOut();
    }
}
