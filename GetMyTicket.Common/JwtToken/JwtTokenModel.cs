﻿using Microsoft.Extensions.Configuration;

namespace GetMyTicket.Common.JwtToken
{
    public class JwtTokenModel
    {
        public const int _AccessTokenTokenExpiration = 3;
        public const int _RefreshTokenTokenExpiration = 120;
        public const string _TokenType = "Bearer";

        public JwtTokenModel(string accessToken, string refreshToken, Guid userId)
        {
            TokenType = _TokenType;
            UserId = userId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            AccessTokenExpires = DateTime.UtcNow.AddMinutes(_AccessTokenTokenExpiration);
            RefresTokenExpires = DateTime.UtcNow.AddMinutes(_RefreshTokenTokenExpiration);
        }

        public string TokenType { get; set; }

        public Guid UserId { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime AccessTokenExpires { get; set; }
        public DateTime RefresTokenExpires { get; set; }
    }
}
