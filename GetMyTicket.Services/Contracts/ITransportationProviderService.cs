﻿using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Common.Entities;

namespace GetMyTicket.Service.Contracts
{
    public interface ITransportationProviderService
    {
        public Task<IEnumerable<GetTransportationProviderDTO>> GetAll();

        public Task<TransportationProvider> Add( CreateTransportationProviderDTO addTpDTO);

        public Task<GetTransportationProviderDTO> GetById(object Id);
    }
}
