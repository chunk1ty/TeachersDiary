using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using TeacherDiary.Clients.Mvc.Controllers;
using TeacherDiary.Clients.Mvc.ViewModels.Account;
using TeacherDiary.Data.Ef;
using TeacherDiary.Services.Identity.Contracts;
using TestStack.FluentMVCTesting;

namespace TeacherDiary.Clients.Mvc.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IIdentityUserManagerService> _mockedIdentityUserManagerService;
        private Mock<IIdentitySignInService> _mockedIdentitySignInService;

        [SetUp]
        public void SetUp()
        {
            _mockedIdentityUserManagerService = new Mock<IIdentityUserManagerService>();
            _mockedIdentitySignInService = new Mock<IIdentitySignInService>();
        }

        [Test]
        public void Constructor_WithNullIdendityUserNamagerService_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() =>
                new AccountController(
                    null,
                    _mockedIdentityUserManagerService.Object
                ));

            StringAssert.Contains("identitySignInService", ex.Message);
        }

        [Test]
        public void Constructor_WithNullIdentitySignInServic_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() =>
                new AccountController(
                    _mockedIdentitySignInService.Object,
                    null));

            StringAssert.Contains("identityUserManagerService", ex.Message);
        }

        [Test]
        public void LoginOnGetRequest_WithAuthenticateUser_ShouldRedirectToDashboardIndex()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object)
            {
                ControllerContext = mockContext.Object
            };
           
            string returnUrl = "url";
           

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRedirectTo<HomeController>(x => x.Index());
        }

        [Test]
        public void LoginOnGetRequest_WithNotAuthenticateUser_ShouldRenderDefaultView()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object)
            {
                ControllerContext = mockContext.Object
            };

            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void LoginOnPostRequest_WithInvalidModelState_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            accountController.ModelState.AddModelError("errorKey", "error");

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("errorKey");
        }

        [Test]
        public void LoginOnPostRequest_WithValidModelStateAndSuccessResult_ShouldRedirectToLocalUrl()
        {
            // Arrange
            Mock<HttpContextBase> mockedHttpContextBase = new Mock<HttpContextBase>();
            UrlHelper _urlHelperMock = new UrlHelper(new RequestContext(mockedHttpContextBase.Object, new RouteData()));

            _mockedIdentitySignInService.Setup(
                 x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInStatus.Success);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            accountController.Url = _urlHelperMock;

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "/";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRedirectTo(returnUrl);
        }

        [Test]
        public void LoginOnPostRequest_WithValidModelStateAndSuccessResult_ShouldRedirectToDashboardIndex()
        {
            // Arrange
            Mock<HttpContextBase> mockedHttpContextBase = new Mock<HttpContextBase>();
            UrlHelper _urlHelperMock = new UrlHelper(new RequestContext(mockedHttpContextBase.Object, new RouteData()));


            _mockedIdentitySignInService.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInStatus.Success);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);
            accountController.Url = _urlHelperMock;

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRedirectTo<HomeController>(x => x.Index());
        }

        [Test]
        public void LoginOnPostRequest_WithValidModelStateAndNotSuccessResult_ShouldRenderDefaultView()
        {
            // Arrange

            _mockedIdentitySignInService.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInStatus.Failure);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("");
        }

        [Test]
        public void RegisterOnGetRequest_WithDefaultFlow_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void RegisterOnPostRequest_WithInvalidModel_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            accountController.ModelState.AddModelError("errorKey", "error");

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("errorKey");
        }

        [Test]
        public void RegisterOnPostRequest_WithValidModelAndSucceededCreateResult_ShouldRedirectToAccountController()
        {
            // Arrange
            _mockedIdentityUserManagerService.Setup(x => x.CreateAsync(It.IsAny<AspNetUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockedIdentitySignInService.Setup(
                    x => x.SignInAsync(It.IsAny<AspNetUser>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(0));

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRedirectTo<AccountController>(x => x.Login(It.IsAny<string>()));
        }

        [Test]
        public void RegisterOnPostRequest_WithValidModelAndFailedCreateResult_ShouldRenderDefaultView()
        {
            // Arrange
            _mockedIdentityUserManagerService.Setup(x => x.CreateAsync(It.IsAny<AspNetUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            _mockedIdentitySignInService.Setup(
                    x => x.SignInAsync(It.IsAny<AspNetUser>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(0));

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRenderDefaultView()
                .WithModel(model);
        }

        [Test]
        public void ChangePasswordOnGetRequest_WithDefaultFlow_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.ChangePassword())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void ChangePasswordOnPostRequest_WithInvalidModelState_ShouldRenderDefaultViewWithErrors()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object);

            accountController.ModelState.AddModelError("errorKey", "error");

            ChangePasswordViewModel model = new ChangePasswordViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.ChangePassword(model))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("errorKey");
        }

        [Test]
        public void ChangePasswordOnPostRequest_WithValidModelStateAndSucceededResultAndExistingUser_ShouldRedirectToHomeController()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            _mockedIdentityUserManagerService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockedIdentityUserManagerService.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new AspNetUser());

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object)
            {
                ControllerContext = mockContext.Object
            };

            ChangePasswordViewModel model = new ChangePasswordViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.ChangePassword(model))
                .ShouldRedirectTo<HomeController>(x => x.Index());

            _mockedIdentitySignInService.Verify(x => x.SignInAsync(It.IsAny<AspNetUser>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void ChangePasswordOnPostRequest_WithValidModelStateAndSucceededResultAndNotExistingUser_ShouldRedirectToHomeController()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            _mockedIdentityUserManagerService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            Func<AspNetUser> nullUserFunc = () => null;

            _mockedIdentityUserManagerService.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(nullUserFunc);

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object)
            {
                ControllerContext = mockContext.Object
            };

            ChangePasswordViewModel model = new ChangePasswordViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.ChangePassword(model))
                .ShouldRedirectTo<HomeController>(x => x.Index());

            _mockedIdentitySignInService.Verify(x => x.SignInAsync(It.IsAny<AspNetUser>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public void ChangePasswordOnPostRequest_WithValidModelStateAndNotSucceededResult_ShouldRenderDefaultViewWithErrors()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            _mockedIdentityUserManagerService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var accountController = new AccountController(
                _mockedIdentitySignInService.Object,
                _mockedIdentityUserManagerService.Object)
            {
                ControllerContext = mockContext.Object
            };


            ChangePasswordViewModel model = new ChangePasswordViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.ChangePassword(model))
                .ShouldRenderDefaultView()
                .WithModel(model);
        }
    }
}
