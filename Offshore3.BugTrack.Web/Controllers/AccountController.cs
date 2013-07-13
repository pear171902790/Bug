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
        private readonly ITransformModel _transformModel;

        public AccountController(IUserLogic userLogic, ICookieHelper cookieHelper,ITransformModel transformModel)
        {
            _userLogic = userLogic;
            _cookieHelper = cookieHelper;
            _transformModel = transformModel;
        }

        //public ActionResult Index()
        //{
        //    ViewBag.UserName = _userLogic.Get(1).UserName;
        //    return View();
        //}

        [HttpGet]
        public ActionResult Login() //*
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel) //*
        {
            try
            {
                var user=_transformModel.ToUserFromLoginViewModel(loginViewModel);
                _userLogic.User = user;
                bool result = _userLogic.AuthenticateUser();
                if (result)
                {
                    user = _userLogic.GetByEmailAndPassword() ?? _userLogic.GetByUserNameAndPassword();
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
        public ActionResult Register() //*
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel) //*
        {
            try
            {
                var user = _transformModel.ToUserFromRegisterViewModel(registerViewModel);
                _userLogic.User = user;
                _userLogic.Register();
                user = _userLogic.GetByEmailAndPassword();
                _cookieHelper.SetAuthCookie(user.UserId.ToString(), false);
                return new RedirectResult(Url.Action("Index", "Users"));
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