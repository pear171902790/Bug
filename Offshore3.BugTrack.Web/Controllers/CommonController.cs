using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offshore3.BugTrack.ViewModels;

namespace Offshore3.BugTrack.Web.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Page(PageViewModel pageViewModel)
        {
            return PartialView(pageViewModel);
        }

    }
}
