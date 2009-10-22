﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;

namespace WebFormsMvp.UnitTests
{
    [TestClass]
    public class PresenterTests
    {
        [TestMethod]
        public void Presenter_Constructor_ShouldIntializeDefaultViewModelForViewTypesThatImplementIViewTModel()
        {
            // Arrange
            var view = new TestViewWithModel();

            // Act
            new TestPresenterWithModelBasedView(view);

            // Assert
            Assert.IsNotNull(view.Model);
        }

        [TestMethod]
        public void Presenter_Constructor_ShouldSupportNonModelBasedViews()
        {
            // Arrange
            var view = MockRepository.GenerateMock<IView>();

            // Act
            new TestPresenter(view);

            // Assert
        }

        [TestMethod]
        public void Presenter_Cache_ReturnsCacheFromHttpContext()
        {
            // Arrange
            var view = MockRepository.GenerateStub<IView>();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();
            var cache = new Cache();
            httpContext.Stub(h => h.Cache).Return(cache);

            // Act
            var presenter = new TestPresenter(view);
            presenter.HttpContext = httpContext;

            // Assert
            Assert.AreSame(cache, presenter.Cache);
        }

        [TestMethod]
        public void Presenter_Request_ReturnsRequestFromHttpContext()
        {
            // Arrange
            var view = MockRepository.GenerateStub<IView>();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();
            var request = MockRepository.GenerateStub<HttpRequestBase>();
            httpContext.Stub(h => h.Request).Return(request);

            // Act
            var presenter = new TestPresenter(view);
            presenter.HttpContext = httpContext;

            // Assert
            Assert.AreSame(request, presenter.Request);
        }

        [TestMethod]
        public void Presenter_Response_ReturnsResponseFromHttpContext()
        {
            // Arrange
            var view = MockRepository.GenerateStub<IView>();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();
            var response = MockRepository.GenerateStub<HttpResponseBase>();
            httpContext.Stub(h => h.Response).Return(response);

            // Act
            var presenter = new TestPresenter(view);
            presenter.HttpContext = httpContext;

            // Assert
            Assert.AreSame(response, presenter.Response);
        }

        [TestMethod]
        public void Presenter_Server_ReturnsServerFromHttpContext()
        {
            // Arrange
            var view = MockRepository.GenerateStub<IView>();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();
            var server = MockRepository.GenerateStub<HttpServerUtilityBase>();
            httpContext.Stub(h => h.Server).Return(server);

            // Act
            var presenter = new TestPresenter(view);
            presenter.HttpContext = httpContext;

            // Assert
            Assert.AreSame(server, presenter.Server);
        }

        [TestMethod]
        public void Presenter_RouteData_ReturnsRouteData()
        {
            // Arrange
            var view = MockRepository.GenerateStub<IView>();
            var httpContext = MockRepository.GenerateStub<HttpContextBase>();
            var request = MockRepository.GenerateStub<HttpRequestBase>();
            httpContext.Stub(h => h.Request).Return(request);

            // Act
            var presenter = new TestPresenter(view);
            presenter.HttpContext = httpContext;
        }

        class TestModel { }

        class TestViewWithModel : IView<TestModel>
        {
            public event EventHandler Load;
            public TestModel Model
            {
                get; set;
            }
        }

        class TestPresenterWithModelBasedView
            : Presenter<TestViewWithModel>
        {
            public TestPresenterWithModelBasedView(TestViewWithModel view)
                : base(view)
            {
            }

            public override void ReleaseView()
            {
                throw new NotImplementedException();
            }
        }

        class TestPresenter : Presenter<IView>
        {
            public TestPresenter(IView view)
                : base(view)
            {
            }

            public override void ReleaseView()
            {
                throw new NotImplementedException();
            }
        }
    }
}