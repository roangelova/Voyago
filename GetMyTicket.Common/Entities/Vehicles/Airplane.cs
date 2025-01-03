using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMyTicket.Common.Entities.Vehicles
{
    public class Airplane:Vehicle
    {
        [Required]
        public AirplaneManufacturer AirplaneManufacturer { get; set; }

        [Required]
        public string Model {  get; set; }

        public DateOnly? ManufacturingDate { get; set; } 
    }
}
