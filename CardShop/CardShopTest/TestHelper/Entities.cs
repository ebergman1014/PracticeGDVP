using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardShop.Models;
using CardShop.Utilities;

namespace CardShopTest.TestHelper
{
    /// <summary>
    /// Has a Method of GetListOfUsers
    /// </summary>
    public class ListOfUsers
    {
        public static List<User> listOfUsers;
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
        public static List<User> GetListOfUsers(int UsersWanted)
        {
            listOfUsers = new List<User>();
            for (int index = 0; index < UsersWanted; index++)
            {
                User user = new User();
                user.UserId = index;
                user.Username = "Tank" + index;
                user.Name = "Frank" + index;
                user.Password = "pass";
                user.Role = "User";
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
    public class User_DiscountTest {
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
        public static UserDiscount CreateCoupon()
        {
            UserDiscount coupon = new UserDiscount();
            UserDiscountUtility udc = new UserDiscountUtility();
            coupon.DiscountRate = 20;
            coupon.EndDate = DateTime.Now;
            coupon.StartDate = DateTime.Now;
            coupon.UserDiscountId = 0;
            coupon.UserId = 10;
            coupon.Reedemed = false;
            coupon.DiscountCode = udc.GenerateCoupon();
            return coupon;
        }

    }
}
