using System.ComponentModel.DataAnnotations;
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
        // find out why there are 4 constructors
        // params NamedParameters added
        [Comparator("StartDate", "End Date must come after Start Date", Comparator.Operator.GreaterThan)]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool Reedemed { get; set; }
        [Display(Name = "Discount Code")]
        public string DiscountCode { get; set; }
    }
}