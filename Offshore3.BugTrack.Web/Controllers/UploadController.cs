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
            var httpPostedFile = Request.Files["UserImage"];
            var imageName = userId + "_temp.jpg";
            var virtualPath = UserConfig.UserImageUrl + imageName;
            var fullPath = Server.MapPath(virtualPath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            httpPostedFile.SaveAs(fullPath);
            return new JsonResult()
                {
                 Data   = new
                        {
                            ReplaceHtml = "<img src=\"" + Url.Content(virtualPath) + "\"/>",
                            IsUpdateUserImage=true
                        }
                };
        }

        public ActionResult BugAttachment(long bugId,long userId)
        {
            var folderName = bugId == 0 ? userId+"_temp":userId+"_"+bugId;
            HttpPostedFileBase httpPostedFile = Request.Files["BugAttachment"];
            var fileName = System.IO.Path.GetFileName(httpPostedFile.FileName);
            var directoryPath = Server.MapPath(Url.Content("~/Content/BugAttachments/" + folderName));
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
