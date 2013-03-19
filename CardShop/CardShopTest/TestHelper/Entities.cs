using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShopTest.TestHelper
{
    /// <summary>
    /// Has a Method of GetListOfUsers
    /// </summary>
    class ListOfUsers
    {
        public static List<UserTest> listOfUsers;
        /// <summary>
        /// Creates a list of Users with:
        /// UserID of n
        ///  a Username of "Tank"+n
        ///  Name of "Frank" + n
        ///  Password of  of pass
        /// UserRole of Role.User
        /// </summary>
        /// <param name="UsersWanted">Number of Users you want to create</param>
        /// <returns></returns>
        public static List<UserTest> GetListOfUsers(int UsersWanted)
        {
            listOfUsers = new List<UserTest>();
            for (int index = 0; index < UsersWanted; index++)
            {
                UserTest user = new UserTest();
                user.UserID = index;
                user.Username = "Tank" + index;
                user.Name = "Frank" + index;
                user.Password = "pass";
                user.UserRole = Role.User;
                listOfUsers.Add(user);
            }
            return listOfUsers;
        }

        public ListOfUsers()
        {

        }
    }
    /// <summary>
    /// UserTest creates a simulation of User class
    /// 
    /// </summary>
    class UserTest
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }

        public UserTest() { }
    }
    /// <summary>
    /// Enum of Various UserRoles, stored as Strings
    /// </summary>
    public enum Role
    {
        Admin = 3, 
        User = 0,
        StoreOwner = 2, 
        RegionalManager = 1
    }

    // will be renamed to coupon
    class User_DiscountTest {
        public int User_DiscountID { get; set; }
        public int UserID { get; set; }
        public int Discount_Rate { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Number_Of_Cards { get; set; }
        public string Discount_Code{get; set;}

        /// <summary>
        /// Create a coupon code, Coupon will have CAPITAL LETTERS ONLY
        /// </summary>
        /// <returns> A String representation of the Coupon Code</returns>
        public static string CreateCouponCode(){
            StringBuilder discountCode = new StringBuilder();
            Random random = new Random();
            for (int index = 0; index < 5; index++) {
                discountCode.Append(Convert.ToChar(random.Next(65, 90)));
            }
            return discountCode.ToString();
        }

        /// <summary>
        /// Set fields on all coupon objects
        /// </summary>
        /// <returns></returns>
        public static User_DiscountTest CreateCoupon()
        {
            User_DiscountTest coupon = new User_DiscountTest();
            coupon.Discount_Rate = 20;
            coupon.End_Date = DateTime.Now;
            coupon.Start_Date = DateTime.Now;
            coupon.User_DiscountID = 0;
            coupon.UserID = 10;
            coupon.Number_Of_Cards = 24;
            coupon.Discount_Code = User_DiscountTest.CreateCouponCode();
            return coupon;
        }

    }
}
