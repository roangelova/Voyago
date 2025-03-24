namespace GetMyTicket.Common.DTOs.Trip
{
    public record SearchTripsDTO(
        string Type,
        string StartDate,
        string EndDate,
        Guid StartCityId,
        Guid EndCityId);
}
