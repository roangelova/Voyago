namespace GetMyTicket.Common.DTOs.Vehicle
{
    public record CreateAirplaneDTO(
        Guid TpProviderId, 
        string Manufacturer, 
        string Model,
        DateOnly ManufacturingDate, 
        int Capacity
        );
    
}
