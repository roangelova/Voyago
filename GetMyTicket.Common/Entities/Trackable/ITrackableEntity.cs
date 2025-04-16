namespace GetMyTicket.Common.Entities.Trackable
{
    public interface ITrackableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
