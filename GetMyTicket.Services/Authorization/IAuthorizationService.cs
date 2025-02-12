using System.Reflection.Metadata;

namespace GetMyTicket.Service.Authorization
{
    public interface IAuthorizationService
    {
        public string GenerateAccessToken(Guid userId);

        public string GenerateRefreshToken();
    }
}
