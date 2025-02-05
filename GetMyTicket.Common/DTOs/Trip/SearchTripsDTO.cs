namespace GetMyTicket.Common.DTOs.Trip
{
    public record SearchTripsDTO(
        string StartDate,
        string EndDate,
        Guid StartCityId,
        Guid EndCityId);
}
