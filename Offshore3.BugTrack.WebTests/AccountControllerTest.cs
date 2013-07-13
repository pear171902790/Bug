using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Moq;
using NUnit.Framework;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.ViewModels;
using Offshore3.BugTrack.Web.Common;
using Offshore3.BugTrack.Web.Controllers;

namespace Offshore3.BugTrack.UnitTests
{
    [TestFixture]
    public class AccountControllerTest
    {
        [Test]
        public void GetLoginTest()
        {
            // Arrange
            //var userLogicMock = new Mock<IUserLogic>();
            //var cookieHelperMock = new Mock<ICookieHelper>();
            //var accountController = new AccountController(userLogicMock.Object, cookieHelperMock.Object);
            
            //var requestMock = new Mock<HttpRequestBase>();
            //requestMock.Setup(r => r.HttpMethod).Returns("GET");

            //var httpContextMock = new Mock<HttpContextBase>();
            //httpContextMock.Setup(hc => hc.Request).Returns(requestMock.Object);

            //var controllerContext = new ControllerContext();
            //controllerContext.HttpContext = httpContextMock.Object;
            //accountController.ControllerContext = controllerContext;

            // Act
            //var result = accountController.Login() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }

        [Test]
        public void PostLoginTestWhenIdIsNotNull()
        {
            //// Arrange
            //var userLogicMock = new Mock<IUserLogic>();
            //var cookieHelperMock = new Mock<ICookieHelper>();
            //var accountController = new AccountController(userLogicMock.Object, cookieHelperMock.Object);
            //var viewModel = new LoginViewModel
            //    {
            //        RememberMe = true
            //    };
            //userLogicMock.Setup(x => x.AuthenticateUser(viewModel)).Returns(1);
            //cookieHelperMock.Setup(x => x.SetAuthCookie("1", true));
            //// Act
            //var result = accountController.Login(viewModel);

            //// Assert
            //Assert.AreEqual(result.GetType(), typeof(RedirectResult));
        }

        [Test]
        public void PostLoginTestWhenIdIsNull()
        {
            //// Arrange
            //var userLogicMock = new Mock<IUserLogic>();
            //var cookieHelperMock = new Mock<ICookieHelper>();
            //var accountController = new AccountController(userLogicMock.Object, cookieHelperMock.Object);
            //var viewModel = new LoginViewModel
            //{
            //    RememberMe = true
            //};
            //userLogicMock.Setup(x => x.AuthenticateUser(viewModel)).Returns(0);
            //cookieHelperMock.Setup(x => x.SetAuthCookie("1", true));

            //// Act
            //var result = accountController.Login(viewModel);

            //// Assert
            //Assert.AreEqual(result.GetType(), typeof(ViewResult));
        }

    }
}
