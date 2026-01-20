using System.Security.Claims;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Http;

namespace GetMyTicket.Service.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
                this.httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            Guid.TryParse(this.httpContextAccessor.HttpContext?.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid userId);

            return userId;
        }
    }
}
