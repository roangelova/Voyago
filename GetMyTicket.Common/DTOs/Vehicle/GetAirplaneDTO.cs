namespace GetMyTicket.Common.DTOs.Vehicle
{
    public record GetAirplaneDTO(
        Guid TpProvider_Id,
        string ManufacturerName,
        string Model,
        int Capacity
        );
}
