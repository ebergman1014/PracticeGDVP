using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Service;
using CardShop.Models;
using CardShopTest.TestHelper;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;
using CardShop.Utilities;
//using System.Linq;
using System.Data.Entity;

namespace CardShopTest.ServiceTests
{
    [TestClass]
    public class ManageStoreServiceTests 
    {
        ManageStoreService adminService;
        Store storeOne;
        Store storeTwo;
        List<Store> stores = StoreTest.CreateStores(2);

        Mock<CardShop.Daos.IPracticeGDVPDao> mockDbContext = new Mock<CardShop.Daos.IPracticeGDVPDao>();
        Mock<IDbSet<Store>> mockDbSet = new Mock<IDbSet<Store>>();

        [TestInitialize]
        public void Setup()
        {
            adminService = new ManageStoreService();
            storeOne = stores[0];
            storeTwo = stores[1];

            adminService.context = mockDbContext.Object;
        }

        [TestMethod]
        public void EditStoreGoodTests()
        {

            bool isSuccess = false;

            mockDbContext.Setup(mock => mock.SaveChanges()).Returns(1);

            Assert.IsTrue(adminService.EditStore(storeOne,
                storeTwo, out isSuccess).DiscountRate ==
                storeTwo.DiscountRate);

            mockDbContext.Verify(mock => mock.SaveChanges());
        }

        [TestMethod]
        public void EditStoreBadTests()
        {
            bool isSuccess = false;
            storeTwo.DiscountRate = 102;
            Assert.IsFalse(adminService.EditStore(storeOne, storeTwo, out isSuccess).
                DiscountRate == storeTwo.DiscountRate);
        }

        [TestMethod]
        public void StoreOwnedGoodTest()
        {
            var adminService = new Mock<ManageStoreService>();
            adminService.Setup(m => m.GetStores(1)).Returns(stores);
            Assert.IsTrue(adminService.Object.OwnedStore(1).StoreId != 0);
        }
    }
}
