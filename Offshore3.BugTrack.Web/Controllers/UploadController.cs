using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.Web.Common;

namespace Offshore3.BugTrack.Web.Controllers
{
    public class UploadController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly ICookieHelper _cookieHelper;

        public UploadController(IUserLogic userLogic,ICookieHelper cookieHelper)
        {
            _userLogic = userLogic;
            _cookieHelper = cookieHelper;
        }

        public ActionResult UserImage()
        {
            var userId = _cookieHelper.GetUserId(Request);
            HttpPostedFileBase httpPostedFile = Request.Files["UserImage"];
            string fileName = System.IO.Path.GetFileName(httpPostedFile.FileName);
            var dotIndex = fileName.IndexOf(".");
            var ext = fileName.Substring(dotIndex);
            string imageName = userId + ext;
            string virtualPath = "~/Content/UserImages/" + imageName;
            string fullPath = Server.MapPath(virtualPath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            httpPostedFile.SaveAs(fullPath);
            _userLogic.UpdateImageUrl(userId, virtualPath);
            return new JsonResult()
                {
                 Data   = new
                        {
                            ReplaceHtml = "<img src=\"" + Url.Content(virtualPath) + "\"/>"
                        }
                };
        }

        public ActionResult BugAttachment(long bugId)
        {
            HttpPostedFileBase httpPostedFile = Request.Files["BugAttachment"];
            var fileName = System.IO.Path.GetFileName(httpPostedFile.FileName);
            var directoryPath = Server.MapPath(Url.Content("~/Content/BugAttachments/" + bugId));
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }
            var fullPath = directoryPath + "/" + fileName;
            httpPostedFile.SaveAs(fullPath);
            return new JsonResult()
            {
                Data = new
                {
                    Status=true
                }
            };
        }

    }
}
