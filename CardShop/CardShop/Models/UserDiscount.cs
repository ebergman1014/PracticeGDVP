//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CardShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserDiscount
    {
        public int UserDiscountId { get; set; }
        public decimal DiscountRate { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string DiscountCode { get; set; }
        public int UserId { get; set; }
    
        public virtual User UserTable { get; set; }
    }
}
