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
        private Mock<ILogger<MenuController>> mockLogger;
        private Mock<IMenuService> mockService;
        private DefaultHttpContext httpContext;
        private TempDataDictionary tempData;

        public MenuControllerTest()
        {
            mockLogger = new Mock<ILogger<MenuController>>();
            mockService = new Mock<IMenuService>();
            httpContext = new DefaultHttpContext();
            mockService.Setup(x => x.Index(null)).Returns(new MenuViewModel());
            mockService.Setup(x => x.Paging(1)).Returns(new MenuViewModel());
            tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        }
        [Fact]
        public void IndexTest()
        {
            var controller = new MenuController(mockLogger.Object, mockService.Object);
            controller.TempData = tempData;

            // Indexメソッドのテスト
            var result = controller.Index();

            // 返却値の型を確認
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<MenuViewModel>(viewResult.ViewData.Model);

            controller.Dispose();
        }
        [Fact]
        public void PagingTest()
        {
            var controller = new MenuController(mockLogger.Object, mockService.Object);
            controller.TempData = tempData;

            // Pagingメソッドのテスト
            var result = controller.Paging(1);

            // 返却値の型を確認
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<MenuViewModel>(viewResult.ViewData.Model);

            controller.Dispose();
        }
        [Fact]
        public void NewEntryTest()
        {
            var controller = new MenuController(mockLogger.Object, mockService.Object);
            controller.TempData = tempData;

            // NewEntryメソッドのテスト
            var result = controller.NewEntry();

            // 遷移先の確認
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("index", actionResult.ActionName);
            Assert.Equal("Create", actionResult.ControllerName);

            controller.Dispose();
        }
    }
}
