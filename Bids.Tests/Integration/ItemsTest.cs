using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Data.Entity;
using Bids.WebUI.DAL;
using Moq;
using Bids.WebUI.Helpers;
using Bids.WebUI.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using Bids.WebUI.Models;
using System.Linq;

namespace Bids.Tests.Integration
{
    [TestClass]
    public class ItemsTest
    {
        [TestInitialize]
        public void InitializBeforeTests()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
            Database.SetInitializer<AuctionContext>(new DropCreateDatabaseAlways<AuctionContext>());
            var context = new AuctionContext();
            context.Database.Initialize(force: true);            
        }

        
        [TestMethod]
        public void Index_Integration()
        {

            var securityHelper = new Mock<ISecurityHelper>();
            securityHelper.Setup(x => x.CurrentUserId).Returns(1);
            var UoW = new UnitOfWork();
            var count = UoW.ItemRepository.Get().Count();
            ItemsController controller = new ItemsController(UoW, securityHelper.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual(count, (result.Model as IEnumerable<Item>).Count());
            
        }
    }
}
