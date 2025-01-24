using Microsoft.Extensions.Configuration;

namespace GetMyTicket.Common.JwtToken
{
    public class JwtTokenModel
    {
        public const int _TokenExpiration = 30;
        public const string _TokenType = "Bearer";

        public JwtTokenModel(string accessToken, string refreshToken)
        {
            TokenType = _TokenType;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = _TokenExpiration;
        }

        public string TokenType { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; } 
    }
}
