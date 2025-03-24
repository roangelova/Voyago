using Microsoft.AspNetCore.Http;

namespace GetMyTicket.Common.DTOs.TP
{
    public record CreateTransportationProviderDTO(
        string Name,
        string Description,
        string Address,
        string Email,
        IFormFile Logo
        );
}
