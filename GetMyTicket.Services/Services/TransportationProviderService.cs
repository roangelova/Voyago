﻿using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Http;

namespace GetMyTicket.Service.Services
{
    public class TransportationProviderService : ITransportationProviderService
    {
        private readonly IUnitOfWork unitOfWork;

        public TransportationProviderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TransportationProvider> Add(CreateTransportationProviderDTO addTpDTO)
        {
            if (string.IsNullOrWhiteSpace(addTpDTO.Name) ||
                string.IsNullOrWhiteSpace(addTpDTO.Description) ||
                string.IsNullOrWhiteSpace(addTpDTO.Email) ||
                string.IsNullOrWhiteSpace(addTpDTO.Address))
            {
                throw new ApplicationError(ResponseConstants.AllFieldsRequired);
            }

            byte[] logo = null;

            if (addTpDTO.Logo != null)
            {
               logo = await GetLogoFromStream(addTpDTO.Logo);
            }

            var entity = new TransportationProvider
            {
                Id = Guid.CreateVersion7(),
                Name = addTpDTO.Name.Trim(),
                Description = addTpDTO.Description.Trim(),
                Address = addTpDTO.Address.Trim(),
                Email = addTpDTO.Email.Trim(),
                Logo = logo
            };

            await unitOfWork.TransportationProviders.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<GetTransportationProviderDTO>> GetAll()
        {
            var data = await unitOfWork.TransportationProviders.GetAllAsync();

            return data.Select(x => new GetTransportationProviderDTO(

               x.Id.ToString(),
                 x.Name,
                x.Description,
                x?.Logo?.Length > 0 ? Convert.ToBase64String(x?.Logo) : null
            ));
        }

        public async Task<GetTransportationProviderDTO> GetById(object Id)
        {
            var entity = await unitOfWork.TransportationProviders.GetByIdAsync(Id);

            if (entity != null)
            {
                return new GetTransportationProviderDTO(
                    entity.Id.ToString(),
                    entity.Name,
                    entity.Description,
                    Convert.ToBase64String(entity.Logo)
                    );
            }

            return null;
        }

        public async Task<TransportationProvider> Update(object id, EditTransportationProvider dto)
        {
            byte[] logo = null;

            if (dto.Logo != null)
            {
                logo = await GetLogoFromStream(dto.Logo);
            }

            TransportationProvider entityToUpdate = await unitOfWork.TransportationProviders.GetByIdAsync(id);

            if (entityToUpdate is null) 
            {
                throw new ApplicationError( string.Format(ResponseConstants.NotFoundError, nameof(TransportationProvider), id.ToString()));
            }

            if (dto.Name is not null) entityToUpdate.Name = dto.Name.Trim();
            if (dto.Description is not null) entityToUpdate.Description = dto.Description.Trim();
            if (dto.Address is not null) entityToUpdate.Address = dto.Address.Trim();
            if (dto.Email is not null) entityToUpdate.Email = dto.Email.Trim();

            entityToUpdate.Logo = logo;

            unitOfWork.TransportationProviders.Update(entityToUpdate);
            await unitOfWork.SaveChangesAsync();

            return entityToUpdate;
        }

        public static async Task<byte[]> GetLogoFromStream(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream.ToArray();
        }
    }
}
