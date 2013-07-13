using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Offshore3.BugTrack.ILogic;

namespace Offshore3.BugTrack.Web.Common
{
    public class CookieHelper:ICookieHelper
    {

        public long GetUserId(HttpRequestBase request)
        {
            return GetId(request);
        }

        private long GetId(HttpRequestBase request)
        {
                HttpCookie userCookie = request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(userCookie.Value);
                return Convert.ToInt64(ticket.Name);
        }

        public void SetAuthCookie(string id, bool isRememberMe)
        {
            FormsAuthentication.SetAuthCookie(id,isRememberMe);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}