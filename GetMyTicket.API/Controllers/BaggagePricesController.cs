﻿using GetMyTicket.Common.DTOs.BaggagePrice;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggagePricesController : ControllerBase
    {
        private readonly IBaggagePriceService baggagePriceService;

        public BaggagePricesController(IBaggagePriceService baggagePriceService)
        {
            this.baggagePriceService = baggagePriceService;
        }


        [HttpGet("{transportationProviderId}")]
        public async Task<List<BaggagePriceDTO>> GetBaggagePricesForTransportationProvider(Guid transportationProviderId, CancellationToken cancellationToken)
        {
            return await baggagePriceService.GetBaggagePricesForTransportationProvider(transportationProviderId, cancellationToken);
        }

        [HttpPost]
        public async Task CreateBaggagePrices(CreateBaggagePricesDTO createBaggagePricesDTO, CancellationToken cancellationToken)
        {
           await baggagePriceService.CreateBaggagePricesForTransportationrovider(createBaggagePricesDTO, cancellationToken);
        }
    }
}
