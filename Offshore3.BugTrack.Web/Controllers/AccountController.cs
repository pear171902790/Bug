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

        [HttpGet]
        public ActionResult Login() 
        {
            //for init Data
            var name = _userLogic.Get(1).UserName;

            var loginViewModel = new LoginViewModel();
            return View(loginViewModel);
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
                loginViewModel.PromptInfo = "Incorrect username or password.";
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
            var registerViewModel=new RegisterViewModel();
            return View(registerViewModel);
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
                var user = _userLogic.Get(userId);
                var webPath = string.Format("{0}{1}.jpg", UserConfig.UserImageUrl, userId);
                var ioPath = Server.MapPath(webPath);
                var imageUrl = System.IO.File.Exists(ioPath) ? webPath : string.Empty;
                var profileViewModel = new ProfileViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    RepeatPassword = user.Password,
                    Gender = user.Gender,
                    Introduction = user.Introduction,
                    CurrentUserName = user.UserName,
                    ImageUrl = imageUrl
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