namespace GetMyTicket.Persistance.Filters
{
    public class TripFilter : BaseFilter
    {
        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public TimeOnly? FromTime { get; set; }

        public TimeOnly? ToTime { get; set; }

        public Guid? FromCityId { get; set; }

        public Guid? ToCityId { get; set; }

        public Guid? ProviderId { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
//TODO: How do we want to handle ORDER By Prop and DESC ASC
//PAGINATION --> BASE FILTER
//Ordering ==> Can we have it in the base filter, do we even want it on the backend?
//Filter?