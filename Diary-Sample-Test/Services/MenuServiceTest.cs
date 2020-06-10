// -----------------------------------------------------------------------
// <copyright file="MenuServiceTest.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
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
        public void IndexTest1()
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
        [Fact]
        public void IndexTest2()
        {
            // Diaryが1件の場合
            mockRepository.Setup(x => x.read(1, MenuViewModel.PageCount))
                .Returns(Enumerable.Range(1, 1).Select(x => new Diary("タイトル", "本文")).ToList());
            mockRepository.Setup(x => x.readCount()).Returns(1);

            // Indexメソッドのテスト
            var result = service.Index("test");

            // 返却値の確認
            var model = Assert.IsType<MenuViewModel>(result);
            Assert.Equal(1, model.TotalNumber);
            Assert.Equal(1, model.TotalPageNumber);
            Assert.Equal("test", model.Notification);
            Assert.Single(model.DiaryList);
        }
        [Fact]
        public void IndexTest3()
        {
            // Diaryが5件の場合
            mockRepository.Setup(x => x.read(1, MenuViewModel.PageCount))
                .Returns(Enumerable.Range(1, 5).Select(x => new Diary("タイトル", "本文")).ToList());
            mockRepository.Setup(x => x.readCount()).Returns(5);

            // Indexメソッドのテスト
            var result = service.Index("test");

            // 返却値の確認
            var model = Assert.IsType<MenuViewModel>(result);
            Assert.Equal(5, model.TotalNumber);
            Assert.Equal(1, model.TotalPageNumber);
            Assert.Equal("test", model.Notification);
            Assert.Equal(5, model.DiaryList.Count);
        }
        [Fact]
        public void PagingTest1()
        {
            // 0件の1ページの場合
            mockRepository.Setup(x => x.read(1, MenuViewModel.PageCount)).Returns(new List<Diary>());
            mockRepository.Setup(x => x.readCount()).Returns(0);

            // Pagingメソッドのテスト
            var result = service.Paging(1);

            // 返却値の確認
            var model = Assert.IsType<MenuViewModel>(result);
            Assert.Equal(0, model.TotalNumber);
            Assert.Equal(0, model.TotalPageNumber);
            Assert.Empty(model.Notification);
            Assert.Empty(model.DiaryList);
        }
        [Fact]
        public void PagingTest2()
        {
            // 1件の1ページの場合
            mockRepository.Setup(x => x.read(1, MenuViewModel.PageCount))
                .Returns(Enumerable.Range(1, 1).Select(x => new Diary("タイトル", "本文")).ToList());
            mockRepository.Setup(x => x.readCount()).Returns(1);

            // Pagingメソッドのテスト
            var result = service.Paging(1);

            // 返却値の確認
            var model = Assert.IsType<MenuViewModel>(result);
            Assert.Equal(1, model.TotalNumber);
            Assert.Equal(1, model.TotalPageNumber);
            Assert.Empty(model.Notification);
            Assert.Single(model.DiaryList);
        }
        [Fact]
        public void PagingTest3()
        {
            // 6件の1ページの場合
            mockRepository.Setup(x => x.read(1, MenuViewModel.PageCount))
                .Returns(Enumerable.Range(1, 5).Select(x => new Diary("タイトル", "本文")).ToList());
            mockRepository.Setup(x => x.readCount()).Returns(6);

            // Pagingメソッドのテスト
            var result = service.Paging(1);

            // 返却値の確認
            var model = Assert.IsType<MenuViewModel>(result);
            Assert.Equal(6, model.TotalNumber);
            Assert.Equal(2, model.TotalPageNumber);
            Assert.Empty(model.Notification);
            Assert.Equal(5, model.DiaryList.Count);
        }
        [Fact]
        public void PagingTest4()
        {
            // 6件の2ページの場合
            mockRepository.Setup(x => x.read(2, MenuViewModel.PageCount))
                .Returns(Enumerable.Range(1, 1).Select(x => new Diary("タイトル", "本文")).ToList());
            mockRepository.Setup(x => x.readCount()).Returns(6);

            // Pagingメソッドのテスト
            var result = service.Paging(2);

            // 返却値の確認
            var model = Assert.IsType<MenuViewModel>(result);
            Assert.Equal(6, model.TotalNumber);
            Assert.Equal(2, model.TotalPageNumber);
            Assert.Empty(model.Notification);
            Assert.Single(model.DiaryList);
        }
    }
}