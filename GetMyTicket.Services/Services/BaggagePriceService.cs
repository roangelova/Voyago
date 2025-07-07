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

        public async Task<List<BaggagePriceDTO>> GetBaggagePricesForTransportationProvider(Guid transportationProviderId, CancellationToken cancellationToken)
        {
            var tp = await unitOfWork.TransportationProviders.GetAsync(x => x.Id == transportationProviderId, true, cancellationToken, x => x.BaggagePrices);

            if (tp is null)
            {
                throw new ApplicationException(string.Format(ResponseConstants.NotFoundError, nameof(TransportationProvider), transportationProviderId));
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

        public async Task CreateBaggagePricesForTransportationrovider(CreateBaggagePricesDTO createBaggagePricesDTO, CancellationToken cancellationToken)
        {
            var tp = await unitOfWork.TransportationProviders.GetByIdAsync(createBaggagePricesDTO.TransportationProviderId);

            if (tp == null)
            {
                throw new ApplicationException(string.Format
                    (ResponseConstants.NotFoundError, nameof(TransportationProvider), createBaggagePricesDTO.TransportationProviderId));
            }

            var missing = Enum.GetValues<BaggageSize>()
                .Except(createBaggagePricesDTO.Prices.Keys);

            if (missing.Any())
            {
                throw new ApplicationException($"Missing prices for: {string.Join(", ", missing)}");
            }

            foreach (var key in createBaggagePricesDTO.Prices)
            {
                await unitOfWork.BaggagePrices.AddAsync(new BaggagePrice
                {
                    Id = Guid.CreateVersion7(),
                    BaggageSize = key.Key,
                    Price = key.Value,
                    Currency = Currency.EUR, //TODO -> implement fix for default currency
                    TransportationProviderId = createBaggagePricesDTO.TransportationProviderId
                }, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
