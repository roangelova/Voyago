﻿using System.Buffers.Text;

namespace GetMyTicket.Common.DTOs.Trip
{
    public class TripSearchResultDTO
    {
        public Guid TripId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Price { get; set; }
        public string EndCityName { get; set; }
        public string StartCityName { get; set; }
        public string TransportationProviderName { get; set; }

        public string TransportationProviderLogo { get; set; }
        public string Currency { get; set; }

        public string TypeOfTrip { get; set; }
    }
}
