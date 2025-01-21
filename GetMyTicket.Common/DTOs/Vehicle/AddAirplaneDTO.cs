namespace GetMyTicket.Common.DTOs.Vehicle
{
    public record AddAirplaneDTO(
        Guid TpProviderId, 
        string Manufacturer, 
        string Model,
        DateOnly ManufacturingDate, 
        int Capacity
        );
    
}
