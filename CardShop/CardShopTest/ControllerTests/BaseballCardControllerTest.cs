using System;
using Moq;
using CardShop.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Collections.Generic;
using CardShop;
using CardShop.Models;
using CardShop.Daos;
using System.Data.Entity;
using CardShop.Service;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class BaseballCardControllerTest
    {
        Mock<IBaseballCardService> mockCardRepository;
        List<BaseballCard> mockCards;

        [TestInitialize]
        public void InitializeTest()
        {
            mockCardRepository = new Mock<IBaseballCardService>();

            mockCards = new List<BaseballCard>();
            mockCards.Add(new BaseballCard() { BaseballCardId = 0, Cost = new Decimal(0.50), Player = "Cole Hammels", Team = "Phillies" });
            mockCards.Add(new BaseballCard() { BaseballCardId = 1, Cost = new Decimal(1.50), Player = "Jimmy Rollins", Team = "Phillies" });
            mockCards.Add(new BaseballCard() { BaseballCardId = 2, Cost = new Decimal(2.50), Player = "Chase Utley", Team = "Phillies" });

            // Setup mock behavior for the GetAllFish() method in our repository
            mockCardRepository.Setup(x => x.GetAllCards()).Returns(mockCards);
            mockCardRepository.Setup(x => x.GetBaseballCard(1)).Returns(mockCards[1]);
        }


        [TestMethod]
        public void TestIndexHasListOfBaseballCards()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
           
            ViewResult result = controller.Index() as ViewResult;
            List<BaseballCard> cards = (List<BaseballCard>)result.ViewData.Model;

            // Test the view model contains a list of fish
            Assert.IsNotNull(cards, "The list of cards does not exist");
            Assert.IsTrue(cards.Count == mockCards.Count);
        }

        [TestMethod]
        public void TestDetailsGetValidBaseballCard()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = 1;

            ViewResult result = controller.Details(baseballCardId) as ViewResult;
            BaseballCard card = (BaseballCard)result.ViewData.Model;

            // Test the view model contains a list of fish
            Assert.IsNotNull(card, "The card does not exist");
            Assert.IsTrue(card.Player == mockCards[1].Player);
        }

        [TestMethod]
        public void TestDetailsGetInvalidBaseballCard()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = -1;

            HttpNotFoundResult result = controller.Details(baseballCardId) as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestBrowseGet()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);

            ViewResult result = controller.Browse() as ViewResult;

            Assert.AreEqual(result.ViewName, "Browse");
        }

        [TestMethod]
        public void TestUploadGet()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);

            ViewResult result = controller.Upload() as ViewResult;

            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void TestUploadValid()
        {
            Assert.Fail("Fail Test First", new NotImplementedException());
        }

        [TestMethod]
        public void TestUploadPostInvalid()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);

            ViewResult result = controller.Upload(null) as ViewResult;

            Assert.AreEqual(result.ViewName, "Upload");
            Assert.IsNotNull(result.TempData["error"]);
        }

        [TestMethod]
        public void TestCreateGet()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);

            ViewResult result = controller.Create() as ViewResult;

            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void TestEditGetValidBaseballCard()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = 1;

            ViewResult result = controller.Edit(baseballCardId) as ViewResult;
            BaseballCard card = (BaseballCard)result.ViewData.Model;

            // Test the view model contains a list of fish
            Assert.IsNotNull(card, "The card does not exist");
            Assert.IsTrue(card.Player == mockCards[1].Player);
        }

        [TestMethod]
        public void TestEditGetInvalidBaseballCard()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = -1;

            HttpNotFoundResult result = controller.Edit(baseballCardId) as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEditPostValidBaseballCard()
        {
            Assert.Fail("Fail Test First", new NotImplementedException());
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = -1;

            HttpNotFoundResult result = controller.Edit(baseballCardId) as HttpNotFoundResult;
        }

        [TestMethod]
        public void TestEditPostInvalidModelState()
        {
            Assert.Fail("Fail Test First", new NotImplementedException());
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = -1;

            HttpNotFoundResult result = controller.Edit(baseballCardId) as HttpNotFoundResult;
        }

        [TestMethod]
        public void TestDeleteGetValidBaseballCard()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = 1;

            ViewResult result = controller.Delete(baseballCardId) as ViewResult;
            BaseballCard card = (BaseballCard)result.ViewData.Model;

            // Test the view model contains a list of fish
            Assert.IsNotNull(card, "The card does not exist");
            Assert.IsTrue(card.Player == mockCards[1].Player);
        }

        [TestMethod]
        public void TestDeleteGetInvalidBaseballCard()
        {
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = -1;

            HttpNotFoundResult result = controller.Delete(baseballCardId) as HttpNotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDeletePostValidBaseballCard()
        {
            Assert.Fail("Fail Test First", new NotImplementedException());
            BaseballCardController controller = new BaseballCardController(mockCardRepository.Object);
            int baseballCardId = 1;

            HttpNotFoundResult result = controller.DeleteConfirmed(baseballCardId) as HttpNotFoundResult;

        }
    }
}
