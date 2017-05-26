using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using TeachersDiary.Clients.Mvc.Controllers;
using TeachersDiary.Clients.Mvc.ViewModels.Account;
using TeachersDiary.Data.Services.Contracts;
using TestStack.FluentMVCTesting;

namespace TeachersDiary.Clients.Mvc.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IAuthenticationService> _mockedAuthenticationService;
        private Mock<ISchoolService> _mockedShoolService;

        [SetUp]
        public void SetUp()
        {
            _mockedAuthenticationService = new Mock<IAuthenticationService>();
            _mockedShoolService = new Mock<ISchoolService>();
        }

        [Test]
        public void Constructor_WithNullIdendityUserNamagerService_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() =>
                new AccountController(
                    null,
                    _mockedAuthenticationService.Object
                ));

            StringAssert.Contains("schoolService", ex.Message);
        }

        [Test]
        public void Constructor_WithNullIdentitySignInServic_ShouldThrowsArgumentNullException()
        {
            var ex = Assert.Catch<ArgumentNullException>(() =>
                new AccountController(
                    _mockedShoolService.Object,
                    null));

            StringAssert.Contains("authenticationService", ex.Message);
        }

        [Test]
        public void LoginOnGetRequest_WithAuthenticateUser_ShouldRedirectToDashboardIndex()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();

            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object)
            {
                ControllerContext = mockContext.Object
            };

            string returnUrl = "url";


            // Act & Assert
            accountController
                .WithCallTo(c => c.Login())
                .ShouldRedirectTo<DashboardController>(x => x.Index());
        }

        [Test]
        public void LoginOnGetRequest_WithNotAuthenticateUser_ShouldRenderDefaultView()
        {
            // Arrange
            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object)
            {
                ControllerContext = mockContext.Object
            };

            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void LoginOnPostRequest_WithInvalidModelState_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

            accountController.ModelState.AddModelError("errorKey", "error");

            LoginViewModel model = new LoginViewModel();
            string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("errorKey");
        }

        [Test]
        public void LoginOnPostRequest_WithValidModelStateAndSuccessResult_ShouldRedirectToLocalUrl()
        {
            // Arrange
            _mockedAuthenticationService.Setup(x => x.LogIn(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(SignInStatus.Success);

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

            LoginViewModel model = new LoginViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model))
                .ShouldRedirectTo<DashboardController>(x => x.Index());
        }

        [Test]
        public void LoginOnPostRequest_WithValidModelStateAndNotSuccessResult_ShouldRenderDefaultView()
        {
            // Arrange

            _mockedAuthenticationService.Setup(x => x.LogIn(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(SignInStatus.Failure);

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

            LoginViewModel model = new LoginViewModel();
            //string returnUrl = "url";

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(model))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("")
                .ThatEquals(Resources.Resources.IncorrentEmailOrPasswordMessage);
        }

        [Test]
        public void RegisterOnGetRequest_WithDefaultFlow_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

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
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

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
            _mockedAuthenticationService.Setup(x => x.CreateAccountAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRedirectTo<DashboardController>(x => x.Index());
        }

        [Test]
        public void RegisterOnPostRequest_WithValidModelAndFailedCreateResult_ShouldRenderDefaultView()
        {
            // Arrange
            var error = "custom error";

            _mockedAuthenticationService.Setup(x => x.CreateAccountAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(error));

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

            RegisterViewModel model = new RegisterViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(model))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("")
                .ThatEquals(error);
        }

        [Test]
        public void ChangePasswordOnGetRequest_WithDefaultFlow_ShouldRenderDefaultView()
        {
            // Arrange
            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

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
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object);

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
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            _mockedAuthenticationService.Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object)
            {
                ControllerContext = mockContext.Object
            };

            ChangePasswordViewModel model = new ChangePasswordViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.ChangePassword(model))
                .ShouldRedirectTo<DashboardController>(x => x.Index());
        }

        [Test]
        public void ChangePasswordOnPostRequest_WithValidModelStateAndSucceededResultAndNotExistingUser_ShouldRedirectToHomeController()
        {
            // Arrange
            var error = "custom error";

            var mockContext = new Mock<ControllerContext>();
            mockContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            _mockedAuthenticationService.Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(error));

            var accountController = new AccountController(
                _mockedShoolService.Object,
                _mockedAuthenticationService.Object)
            {
                ControllerContext = mockContext.Object
            };


            ChangePasswordViewModel model = new ChangePasswordViewModel();

            // Act & Assert
            accountController
                .WithCallTo(c => c.ChangePassword(model))
                .ShouldRenderDefaultView()
                .WithModel(model)
                .AndModelError("")
                .ThatEquals(error);
        }
    }
}
