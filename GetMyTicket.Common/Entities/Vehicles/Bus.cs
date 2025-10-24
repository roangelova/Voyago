using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Common.Entities.Contracts;

namespace GetMyTicket.Common.Entities.Vehicles
{
    public class Bus : Vehicle
    {
        public bool HasToiletOnBoard { get; set; }

        public override AmenitiesViewModel GetAmenities()
        {
            return new AmenitiesViewModel
            {
                HasToiletOnBoard = this.HasToiletOnBoard,
            };
        }
    }
}
