using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offshore3.BugTrack.Web.Controllers
{
    public class DownloadController : Controller
    {
        //
        // GET: /Download/

        public ActionResult DownloadAttachment(long bugId, string name)
        {
            var virtualPath = string.Format("~/Content/BugAttachments/{0}/{1}", bugId.ToString(), name);
            var path = Server.MapPath(Url.Content(virtualPath));
            var attachmentName = Url.Encode(System.IO.Path.GetFileName(path));
            return File(path, "application/x-zip-compressed", attachmentName);
        }

    }
}
