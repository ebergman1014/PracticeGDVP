using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Service;
using CardShop.Models;
using CardShopTest.TestHelper;
using Moq;
using CardShop.Daos;
using System.Collections.Generic;
using CardShop.Utilities;

namespace CardShopTest.ServiceTests
{
    [TestClass]
    public class ManageStoreServiceTests
    {
        ManageStoreService adminService;
        Store storeOne;
        Store storeTwo;
        List<Store> stores;

        Mock<IPracticeGDVPDao> mockDbContext = new Mock<IPracticeGDVPDao>();

        [TestInitialize]
        public void Setup()
        {
            adminService = new ManageStoreService();
            stores = StoreTest.CreateStores(2);
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
            bool isSuccess = false ;
            storeTwo.DiscountRate = 102;
            Assert.IsFalse(adminService.EditStore(storeOne, storeTwo, out isSuccess).
                DiscountRate == storeTwo.DiscountRate);
        }

        [TestMethod]
        public void StoreOwnedGoodTest()
        {

            // Work in progress.
        //    var mockDbContext = new Mock<IPracticeGDVPDao>();
          //  adminService.context = mockDbContext.Object;

            //mockDbContext.Setup(mock => mock.Stores().
            // Where(s => s.UserId == 1).ToList()).Returns(stores);

          //  Assert.IsTrue(adminService.OwnedStore(1).StoreId != 0);
        }
    }
}
