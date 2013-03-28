using System;
using System.Collections.Generic;
using System.Collections;
using CardShop.Daos;
using System.Linq;
using System.Linq.Expressions;
using CardShop.Models;
using CardShop.Service.Admin;
using CardShopTest.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using CardShop.Auth;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text;
using CardShop.Utilities;



namespace CardShopTest.ServiceTests.Admin
{
    [TestClass]
    public class ManageUserServiceTests
    {
        // manage
        private ManageUserService manageUserService;

        //constants needed for tests
        private const int USER_FOUR = 4;
        private const int FIRST_USER = 0;
        private const string stupidPath = "you are a dumb path";

        // MOCKS
        Mock<IPracticeGDVPDao> mockIPracticeGDVPDao;
        Mock<IDbSet<User>> mockDbSetUser;
        Mock<IDbSet<Store>> mockDbSetStore;
        Mock<IDbSet<webpages_Roles>> mockDbSetWebpageRoles;
        Mock<IFactory> mockStoreFactory;
        Mock<ManageUserService> mockManageUserService;

        // Objects needed for various tests
        private List<User> listOfUsersTest = ListOfUsers.GetListOfUsers(USER_FOUR);
        private Store store = StoreTest.CreateStores(1)[0];
        private User userTest = ListOfUsers.GetListOfUsers(USER_FOUR)[FIRST_USER];
        // for the out parameters
        bool isSuccess;

        [TestInitialize]
        public void Setup()
        {
            // set service
            manageUserService = new ManageUserService();

            // set our field object to the mock object
            mockIPracticeGDVPDao = new Mock<IPracticeGDVPDao>();
            mockDbSetUser = new Mock<IDbSet<User>>();
            mockDbSetStore = new Mock<IDbSet<Store>>();
            mockDbSetWebpageRoles = new Mock<IDbSet<webpages_Roles>>();
            mockStoreFactory = new Mock<IFactory>();
            mockManageUserService = new Mock<ManageUserService>();

            // set Factory
            Factory.Instance = mockStoreFactory.Object;
            mockStoreFactory.Setup(f => f.Create<PracticeGDVPDao, IPracticeGDVPDao>()).Returns(mockIPracticeGDVPDao.Object);

            // set mock for Entities
            manageUserService.context = mockIPracticeGDVPDao.Object;

            // success is false
            isSuccess = false;
        }

        /// <summary>
        /// ManageUserService.DeleteUser() success
        /// </summary>
        [TestMethod]
        public void DeleteUserPassTest()
        {
            // set mocks
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users().Find(USER_FOUR)).Returns(userTest);
            // expected object returned and success
            Assert.AreEqual<User>(manageUserService.DeleteUser(USER_FOUR, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);
            // verify all methods are called.
            mockIPracticeGDVPDao.Verify(mockObject => mockObject.SaveChanges());
        }
        /// <summary>
        /// ManageUserService.DeleteUser() fail
        /// </summary>
        [TestMethod]
        public void DeleteUserFailTest()
        {

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users().Find(USER_FOUR)).Returns((User)null);
            Assert.AreEqual<User>(manageUserService.DeleteUser(USER_FOUR, out isSuccess), null);
            Assert.IsFalse(isSuccess);
        }
        /// <summary>
        /// ManageUserService.GetAllUser() success!
        /// </summary>
        [TestMethod]
        public void GetAllUsersPassTest()
        {
            // needed for Lambda expression
            mockManageUserService.Setup(m => m.GetAllUsersList()).Returns(listOfUsersTest);
            // assert object is of type exptec and success!
            Assert.AreSame(listOfUsersTest, mockManageUserService.Object.GetAllUsers(out isSuccess));
            Assert.IsTrue(isSuccess);

        }
        /// <summary>
        /// ManageUserService.GetAllUsers fail!!
        /// </summary>
        [TestMethod]
        public void GetAllUsersFailTest()
        {

            // expect empty list on fail
            listOfUsersTest = new List<User>();
            // needed to test Lambda expression
            mockManageUserService.Setup(m => m.GetAllUsersList()).Returns(listOfUsersTest);
            // verify object is of expected type, and fail
            Assert.AreSame(listOfUsersTest, mockManageUserService.Object.GetAllUsers(out isSuccess));
            Assert.IsFalse(isSuccess);

        }
        /// <summary>
        /// ManageUserService.GetUser(), pass
        /// </summary>
        [TestMethod]
        public void GetUserGoodTest()
        {
            // set objects
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns(userTest);
            // expected result and success!
            Assert.AreSame(manageUserService.GetUser(USER_FOUR, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);

        }
        /// <summary>
        /// ManageUserService.GetUser(), fail
        /// </summary>
        [TestMethod]
        public void GetUserBadTest()
        {
            // set objects
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns((User)null);
            // expected return and fail
            Assert.AreSame(manageUserService.GetUser(USER_FOUR, out isSuccess), null);
            Assert.IsFalse(isSuccess);
        }
        /// <summary>
        /// Create a User. Only success ;-)
        /// </summary>
        [TestMethod]
        public void CreateUserTest()
        {
            // mock methods
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Add(userTest)).Returns((User)userTest);
            // verify success and object is expected object
            Assert.AreSame(manageUserService.CreateUser(userTest, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);
            // verify all methods are called
            mockIPracticeGDVPDao.Verify(mockObject => mockObject.SaveChanges());

        }
        /// <summary>
        /// ManageUserService.GetRoleView() does return success (it cannot fails!!).
        /// famous last words. HA.
        /// </summary>
        [TestMethod]
        public void GetRoleViewTest()
        {
            // mock method call
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.webpages_Roles()).Returns(mockDbSetWebpageRoles.Object);
            // returns expected type and succes is true
            Assert.IsInstanceOfType(manageUserService.GetRoleView(out isSuccess), typeof(IEnumerable));
            Assert.IsTrue(isSuccess);
            // verify all methods are called
            mockIPracticeGDVPDao.Verify(mockObject => mockObject.webpages_Roles());
        }
        /// <summary>
        /// ManageUserService.EditUser() fail
        /// </summary>
        [TestMethod]
        public void EditUserFailTest()
        {
            // mock bad inputs. Returns a (User)null
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(userTest.UserId)).Returns((User)null);
            // verify expected result
            Assert.AreSame(manageUserService.EditUser(userTest, out isSuccess), userTest);
            // verify success is false.
            Assert.IsFalse(isSuccess);

        }
        /// <summary>
        /// ManageUserService.EditUser() succesful
        /// </summary>
        [TestMethod]
        public void EditUserPassTest()
        {
            // mock the outputs
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            // return the userTest (which is a full User) on context.Users().Find(User.UserId)
            mockDbSetUser.Setup(mockObject => mockObject.Find(userTest.UserId)).Returns(userTest);
            // verify object returned is same as expected
            Assert.AreSame(manageUserService.EditUser(userTest, out isSuccess), userTest);
            // verify success
            Assert.IsTrue(isSuccess);
            // verify all methods are called
            mockIPracticeGDVPDao.Verify(mockObject => mockObject.SaveChanges());
        }

        /// <summary>
        /// ManageUserService.CreateStore() fail
        /// </summary>
        [TestMethod]
        public void CreateStoreStoreOwnerFailTest()
        {
            // set RoleId to "Store owner"
            userTest.RoleId = 2;
            mockIPracticeGDVPDao.Setup(m => m.Stores()).Returns(mockDbSetStore.Object);
            // return a list, that contains store. So user is already an owner of a store!
            mockManageUserService.Setup(m => m.FindStore(userTest)).Returns(new List<Store>() { store });
            // verify expected object is null!
            Assert.AreSame(null, mockManageUserService.Object.CreateStore(userTest));

        }
        /// <summary>
        /// ManageUserService.CreateStore() succesful
        /// </summary>
        [TestMethod]
        public void CreateStoreStoreOwnerPassTest()
        {
            store = new Store();
            userTest.RoleId = 2;

            // set the entity
            mockManageUserService.Object.context = mockIPracticeGDVPDao.Object;
            // return new store object on factory create
            mockStoreFactory.Setup(m => m.Create<Store>()).Returns(store);

            mockIPracticeGDVPDao.Setup(m => m.Stores()).Returns(mockDbSetStore.Object);
            // return empty list, user currently owns no stores!
            mockManageUserService.Setup(m => m.FindStore(userTest)).Returns(new List<Store>());
            // object expected
            Assert.AreSame(store, mockManageUserService.Object.CreateStore(userTest));

            // verify all methods are called 
            mockIPracticeGDVPDao.Verify(m => m.SaveChanges());
            mockDbSetStore.Verify(m => m.Add(store));
            // verify new store stored contains same UserId
            Assert.AreEqual(userTest.UserId, store.UserId);
        }
        /// <summary>
        /// ManageUserService.ActAsUser() mocks a fail result
        /// </summary>
        [TestMethod]
        public void ActAsUserFailTest()
        {
            // mock the necessary objects
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns((User)null);
            // run test
            manageUserService.ActAsUser(USER_FOUR, out isSuccess);
            // verify a false result
            Assert.IsFalse(isSuccess);
        }

    }
}
