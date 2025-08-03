namespace GetMyTicket.Common.DTOs.Discount
{
    public class GetDiscountDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DiscountType { get; set; }

        public double Value { get; set; }

        public double? MinimumAmount { get; set; }

        public bool HasExpirationDate { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
