using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Offshore3.BugTrack.Web.Common
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            var userCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(userCookie.Value);
            long userId;
            if ((!string.IsNullOrEmpty(ticket.Name)) && long.TryParse(ticket.Name, out userId))
            {
                result = true;
            }
            return result;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (filterContext != null)
                {
                    var context = filterContext.HttpContext;
                    if (!AuthorizeCore(context))
                    {
                        filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
            }
            
        }
    }
}