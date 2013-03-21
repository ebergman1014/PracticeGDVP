﻿using System.ComponentModel.DataAnnotations;
using CardShop.Models.Validator;

namespace CardShop.Models
{
    [MetadataType(typeof(UserDiscountMetaData))]
    public partial class UserDiscount
    {

    }

    public class UserDiscountMetaData
    {
        public int UserDiscountId { get; set; }
        [Required]
        [Display(Name = "Discount Rate")]
        public decimal DiscountRate { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }
        [Required]
        [DateValidator("StartDate")]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool Reedemed { get; set; }
        [Display(Name = "Discount Code")]
        public string DiscountCode { get; set; }
    }
}