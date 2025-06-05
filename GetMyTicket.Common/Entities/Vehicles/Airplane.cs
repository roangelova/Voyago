using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;

namespace GetMyTicket.Common.Entities.Vehicles
{
    public class Airplane:Vehicle
    {
        public AirplaneManufacturer AirplaneManufacturer { get; set; }

        public required string Model {  get; set; }

        public DateOnly? ManufacturingDate { get; set; } 
    }
}
