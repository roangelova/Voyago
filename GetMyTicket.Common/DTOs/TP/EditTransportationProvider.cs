using Microsoft.AspNetCore.Http;

namespace GetMyTicket.Common.DTOs.TP
{
    public class EditTransportationProvider
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public IFormFile? Logo { get; set; }
    }
}
