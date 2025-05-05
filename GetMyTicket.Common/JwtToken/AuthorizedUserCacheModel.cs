using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GetMyTicket.Common.JwtToken
{
    public class AuthorizedUserCacheModel
    {
        public Guid Id { get; set; }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
