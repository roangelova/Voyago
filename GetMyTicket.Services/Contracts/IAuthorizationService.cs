using System.Reflection.Metadata;

namespace GetMyTicket.Service.Contracts
{
    public interface IAuthorizationService
    {
        public string GenerateAccessToken(Guid userId);

        public string GenerateRefreshToken();
    }
}
