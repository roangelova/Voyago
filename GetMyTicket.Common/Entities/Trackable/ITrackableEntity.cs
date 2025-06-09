namespace GetMyTicket.Common.Entities.Trackable
{
    /// <summary>
    /// Properties are dynamically set using override SaveChanges, depending on the type of EntityState. No manual set is needed. 
    /// </summary>
    public interface ITrackableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
