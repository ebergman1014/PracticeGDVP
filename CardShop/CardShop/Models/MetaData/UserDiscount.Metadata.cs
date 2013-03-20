using System.ComponentModel.DataAnnotations;

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
        public decimal DiscountRate { get; set; }
        [Required]
        public System.DateTime StartDate { get; set; }
        [Required]
        public System.DateTime EndDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool Reedemed { get; set; }
        public string DiscountCode { get; set; }
    }
}