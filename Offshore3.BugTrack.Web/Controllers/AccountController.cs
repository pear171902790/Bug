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
                var email = loginViewModel.UserNameOrEmail;
                var password = loginViewModel.Password;
                var username = loginViewModel.UserNameOrEmail;
                var result = _userLogic.AuthenticateUser(email,username,password);
                if (result)
                {
                    var user = _userLogic.GetByEmailAndPassword(email, password) ??
                               _userLogic.GetByUserNameAndPassword(username, password);
                    _cookieHelper.SetAuthCookie(Convert.ToString(user.UserId), false);
                    return new RedirectResult(Url.Action("Index", "Users"));
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
                    return new RedirectResult(Url.Action("Index", "Users"));
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
    }
}