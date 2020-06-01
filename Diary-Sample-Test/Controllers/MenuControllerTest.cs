// -----------------------------------------------------------------------
// <copyright file="MenuControllerTest.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Diary_Sample.Controllers;
using Diary_Sample.Models;
using Diary_Sample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Diary_Sample_Test.Controllers
{
    public class MenuControllerTest
    {
        [Fact]
        public void IndexTest()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<MenuController>>();
            var mockService = new Mock<IMenuService>();
            mockService.Setup(x => x.Index(null)).Returns(new MenuViewModel());
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new MenuController(mockLogger.Object, mockService.Object);
            controller.TempData = tempData;

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<MenuViewModel>(viewResult.ViewData.Model);
            controller.Dispose();
        }
    }
}
