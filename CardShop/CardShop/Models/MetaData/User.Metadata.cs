using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {

    }
    
    public class UserMetaData
    {

        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> RoleId { get; set; }
    
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual webpages_Roles webpages_Roles { get; set; }
        public virtual ICollection<UserDiscount> UserDiscounts { get; set; }
    }
}
