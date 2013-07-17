using System;
using System.Web.Mvc;
using System.Web.Security;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.ViewModels;
using Offshore3.BugTrack.Web.Common;

namespace Offshore3.BugTrack.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICookieHelper _cookieHelper;
        private readonly IUserLogic _userLogic;

        public AccountController(IUserLogic userLogic, ICookieHelper cookieHelper)
        {
            _userLogic = userLogic;
            _cookieHelper = cookieHelper;
        }

        //public ActionResult Index()
        //{
        //    ViewBag.UserName = _userLogic.Get(1).UserName;
        //    return View();
        //}

        [HttpGet]
        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel) 
        {
            try
            {
                var user = new User
                    {
                        Email = loginViewModel.UserNameOrEmail,
                        Password = loginViewModel.Password,
                         UserName= loginViewModel.UserNameOrEmail
                    };
                var result = _userLogic.AuthenticateUser(user);
                if (result)
                {
                    user = _userLogic.GetByEmailAndPassword(user.Email, user.Password) ??
                               _userLogic.GetByUserNameAndPassword(user.UserName, user.Password);
                    _cookieHelper.SetAuthCookie(Convert.ToString(user.UserId), false);
                    return new RedirectResult(Url.Action("Index", "Project"));
                }
                loginViewModel.PromptInfo = "username or password is error";
                return View(loginViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel) 
        {
            try
            {
                if (_userLogic.GetByUserName(registerViewModel.UserName) != null || _userLogic.GetByEmail(registerViewModel.Email)!=null)
                {
                    registerViewModel.PromptInfo = "username or email is already used";
                    return View(registerViewModel);
                }
                var user = new User
                {
                    UserName = registerViewModel.UserName,
                    Password = registerViewModel.Password,
                    Email = registerViewModel.Email
                };
               
                if (_userLogic.Register(user))
                {
                    user = _userLogic.GetByEmailAndPassword(user.Email,user.Password);
                    _cookieHelper.SetAuthCookie(Convert.ToString(user.UserId), false);
                    return new RedirectResult(Url.Action("Index", "Project"));
                }
                registerViewModel.PromptInfo = "Registration failed";
                return View(registerViewModel);
            }
            catch 
            {
                return View("Error");
            }
        }

        public ActionResult SignOut()
        {
            _cookieHelper.SignOut();
            return new RedirectResult(FormsAuthentication.LoginUrl);
        }


        [HttpGet]
        public ActionResult Profiles()
        {
            try
            {
                var userId = _cookieHelper.GetUserId(Request);
                var user = _userLogic.GetByUserName(userId);
                var profileViewModel = new ProfileViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    RepeatPassword = user.Password,
                    Gender = user.Gender,
                    Introduction = user.Introduction,
                    CurrentUserName = user.UserName,
                    ImageUrl = string.Format("{0}{1}.jpg", UserConfig.UserImageUrl, userId)
                };
                return View(profileViewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Profiles(ProfileViewModel profileViewModel)
        {
            try
            {
                var user = new User
                {
                    UserName = profileViewModel.UserName,
                    Email = profileViewModel.Email,
                    Password = profileViewModel.Password,
                    Gender = profileViewModel.Gender,
                    Introduction = profileViewModel.Introduction,
                    UserId = _cookieHelper.GetUserId(Request),
                };
                _userLogic.Update(user);

                if (profileViewModel.IsUpdateUserImage)
                {
                    var ioPath = Server.MapPath(UserConfig.UserImageUrl);
                    var imgPath = ioPath + user.UserId + ".jpg";
                    var tempImgPath = ioPath + user.UserId + "_temp.jpg";
                    System.IO.File.Delete(imgPath);
                    System.IO.File.Move(tempImgPath, imgPath);
                }

                return new RedirectResult(Url.Action("Index", "Project"));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}