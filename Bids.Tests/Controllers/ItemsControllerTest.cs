using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bids.WebUI.Controllers;
using System.Web.Mvc;
using Moq;
using Bids.WebUI.DAL;
using Bids.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using Bids.WebUI.Helpers;

namespace Bids.Tests.Controllers
{
    [TestClass]
    public class ItemsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var items = new Item[] { new Item { AuctionEndDate = DateTime.Now, Title = "Title" } };
            var itemRepo = new Mock<IGenericRepository<Item>>();
            itemRepo.Setup(x => x.Get(null, null, "")).Returns(items);
            var moqUoW = new Mock<IUnitOfWork>();
            moqUoW.Setup(x => x.ItemRepository).Returns(itemRepo.Object);
            //moqUoW.Setup(x => x.ItemRepository).Returns(itemRepo);

            var securityHelper = new Mock<ISecurityHelper>();
            securityHelper.Setup(x => x.CurrentUserId).Returns(1);
            ItemsController controller = new ItemsController(moqUoW.Object, securityHelper.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
            Assert.AreEqual(1, (result.Model as IEnumerable<Item>).Count());
        }

        [TestMethod]
        public void TestCreate()
        {
            var items = new Item[] { new Item { AuctionEndDate = DateTime.Now, Title = "Title" } };
            var users = new UserProfile[] { new UserProfile { UserId = 1, UserName = "UserName TEst" } };

            var itemRepo = new Mock<IGenericRepository<Item>>();
            itemRepo.Setup(x => x.Get(null, null, "")).Returns(items);

            var memberRepo = new Mock<IGenericRepository<UserProfile>>();
            memberRepo.Setup(x => x.Get(null, null, "")).Returns(users);

            var moqUoW = new Mock<IUnitOfWork>();
            moqUoW.Setup(x => x.ItemRepository).Returns(itemRepo.Object);
            moqUoW.Setup(x => x.MemberRepository).Returns(memberRepo.Object);

            var securityHelper = new Mock<ISecurityHelper>();
            securityHelper.Setup(x => x.CurrentUserId).Returns(1);
            ItemsController controller = new ItemsController(moqUoW.Object, securityHelper.Object);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(1, ((Item)result.Model).UserId);
        }

        [TestMethod]
        public void TestInvalidPostCreate()
        {
            var items = new Item[] { new Item { AuctionEndDate = DateTime.Now, Title = "Title" } };
            var itemRepo = new Mock<IGenericRepository<Item>>();
            itemRepo.Setup(x => x.Get(null, null, "")).Returns(items);
            var moqUoW = new Mock<IUnitOfWork>();
            moqUoW.Setup(x => x.ItemRepository).Returns(itemRepo.Object);

            var securityHelper = new Mock<ISecurityHelper>();
            securityHelper.Setup(x => x.CurrentUserId).Returns(1);
            ItemsController controller = new ItemsController(moqUoW.Object, securityHelper.Object);


            controller.ModelState.AddModelError("Title", "Error Message");

            // Act
            var result = controller.Create(new Item());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(ViewResult), result.GetType());
        }

        [TestMethod]
        public void TestDetails()
        {
            var items = new Item[] { new Item { AuctionEndDate = DateTime.Now, Title = "Title" } };
            var itemRepo = new Mock<IGenericRepository<Item>>();
            itemRepo.Setup(x => x.Get(null, null, "")).Returns(items);
            itemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Item { ItemID = 1, Title = "Test Title", UserId = 10 });

            var moqUoW = new Mock<IUnitOfWork>();
            moqUoW.Setup(x => x.ItemRepository).Returns(itemRepo.Object);

            var securityHelper = new Mock<ISecurityHelper>();
            securityHelper.Setup(x => x.CurrentUserId).Returns(1);
            ItemsController controller = new ItemsController(moqUoW.Object, securityHelper.Object);


            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((Item)result.Model).ItemID);
            Assert.AreEqual("Test Title", ((Item)result.Model).Title);
            Assert.AreEqual(10, ((Item)result.Model).UserId);

        }


        [TestMethod]
        public void TestEdit()
        {
            var items = new Item[] { new Item { AuctionEndDate = DateTime.Now, Title = "Title" } };
            var users = new UserProfile[] { new UserProfile { UserId = 1, UserName = "UserName TEst" } };

            var itemRepo = new Mock<IGenericRepository<Item>>();
            itemRepo.Setup(x => x.Get(null, null, "")).Returns(items);

            var memberRepo = new Mock<IGenericRepository<UserProfile>>();
            memberRepo.Setup(x => x.Get(null, null, "")).Returns(users);

            var moqUoW = new Mock<IUnitOfWork>();
            moqUoW.Setup(x => x.ItemRepository).Returns(itemRepo.Object);
            moqUoW.Setup(x => x.MemberRepository).Returns(memberRepo.Object);

            var securityHelper = new Mock<ISecurityHelper>();
            securityHelper.Setup(x => x.CurrentUserId).Returns(1);
            ItemsController controller = new ItemsController(moqUoW.Object, securityHelper.Object);

            // Act
            var result = controller.Edit(new Item { ItemID = 1, Title = "Title"});

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(RedirectToRouteResult),result.GetType());
        }

        [TestMethod]
        public void TestInvalidPostEdit()
        {
            var items = new Item[] { new Item { AuctionEndDate = DateTime.Now, Title = "Title" } };
            var itemRepo = new Mock<IGenericRepository<Item>>();
            itemRepo.Setup(x => x.Get(null, null, "")).Returns(items);
            var moqUoW = new Mock<IUnitOfWork>();
            moqUoW.Setup(x => x.ItemRepository).Returns(itemRepo.Object);

            var securityHelper = new Mock<ISecurityHelper>();
            securityHelper.Setup(x => x.CurrentUserId).Returns(1);
            ItemsController controller = new ItemsController(moqUoW.Object, securityHelper.Object);


            controller.ModelState.AddModelError("Title", "Error Message");

            // Act
            var result = controller.Edit(new Item{ ItemID = 5});

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(ViewResult), result.GetType());
            Assert.AreEqual(5, ((Item)((ViewResult) result).Model).ItemID);
        }
    }
}
