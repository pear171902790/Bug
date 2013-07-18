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
            get { return "~/Content/UserImages/"; }
        }

        public static string InitialPassword
        {
            get { return "123456"; }
        }

        public static string InitialEmailExt
        {
            get { return "@shinetechchina.com"; }
        }
    }
}