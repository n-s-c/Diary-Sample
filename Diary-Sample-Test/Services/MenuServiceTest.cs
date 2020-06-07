// -----------------------------------------------------------------------
// <copyright file="MenuServiceTest.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using Diary_Sample.Entities;
using Diary_Sample.Models;
using Diary_Sample.Repositories;
using Diary_Sample.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Diary_Sample_Test.Services
{
    public class MenuServiceTest
    {
        private Mock<ILogger<MenuService>> mockLogger;
        private Mock<IDiaryRepository> mockRepository;
        private MenuService service;
        public MenuServiceTest()
        {
            mockLogger = new Mock<ILogger<MenuService>>();
            mockRepository = new Mock<IDiaryRepository>();
            service = new MenuService(mockLogger.Object, mockRepository.Object);
        }
        [Fact]
        public void IndexTest()
        {
            // Diaryが0件の場合
            mockRepository.Setup(x => x.read(1, MenuViewModel.PageCount)).Returns(new List<Diary>());
            mockRepository.Setup(x => x.readCount()).Returns(0);

            // Indexメソッドのテスト
            var result = service.Index("test");

            // 返却値の確認
            var model = Assert.IsType<MenuViewModel>(result);
            Assert.Equal(0, model.TotalNumber);
            Assert.Equal(0, model.TotalPageNumber);
            Assert.Equal("test", model.Notification);
            Assert.Empty(model.DiaryList);
        }
    }
}