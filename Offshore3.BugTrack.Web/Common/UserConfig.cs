using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Offshore3.BugTrack.Web.Common
{
    public static class UserConfig
    {
        public static string UserImageUrl
        {
            get
            {
                var dict = ConfigurationManager.GetSection("UserImageUrl") as IDictionary;
                return dict["url"].ToString();
            }
        }
    }
}