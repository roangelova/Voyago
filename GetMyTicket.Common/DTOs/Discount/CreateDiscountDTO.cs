using System.ComponentModel.DataAnnotations;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;
namespace GetMyTicket.Common.DTOs.Discount
{
    public class CreateDiscountDTO
    {
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public string DiscountType { get; set; }

        [Range(1,50)]
        public double Value { get; set; }

        [Range(1,1000)]
        public double? MinimumAmount { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
