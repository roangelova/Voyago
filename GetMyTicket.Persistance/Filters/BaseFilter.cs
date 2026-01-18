namespace GetMyTicket.Persistance.Filters
{
    public class BaseFilter
    {
        //Entity related
        public bool? IsDeleted { get; set; }

        //Pagination
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
