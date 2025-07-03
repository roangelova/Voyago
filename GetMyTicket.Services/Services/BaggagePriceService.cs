using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.BaggagePrice;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Enum;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class BaggagePriceService : IBaggagePriceService
    {
        private readonly IUnitOfWork unitOfWork;

        public BaggagePriceService(IUnitOfWork unitOfWork)
        {
                this.unitOfWork = unitOfWork;
        }

        public async Task<List<BaggagePriceDTO>> GetBaggagericesForTransportationProvider(Guid transportationProviderId, CancellationToken cancellationToken)
        {
            var tp = await unitOfWork.TransportationProviders.GetAsync(x => x.Id == transportationProviderId, true, cancellationToken, x => x.BaggagePrices);

            if (tp is null) 
            { 
                throw new ApplicationException(string.Format(ResponseConstants.NotFoundError, nameof(TransportationProvider),  transportationProviderId));
            }

            if (tp.BaggagePrices.Count is 0)
            {
                //return an empty price list if there are no prices for some reason; Let frontend deal with lack of information
                return new List<BaggagePriceDTO> {
                    new() { Size = Enum.GetName<BaggageSize>(BaggageSize.CarryOn), Price = 0 },
                    new() { Size = Enum.GetName<BaggageSize>(BaggageSize.Small), Price = 0 },
                    new() { Size = Enum.GetName<BaggageSize>(BaggageSize.Large), Price = 0 }};
            }

            return tp.BaggagePrices.Select(x => new BaggagePriceDTO
            {
                Size = Enum.GetName<BaggageSize>(x.BaggageSize),
                Price = x.Price 
            }).ToList();
        }
    }
}
