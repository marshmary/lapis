using System;

namespace Lapis.API.Dtos
{
    public class Token
    {
        public Token(string refreshToken, DateTime expire)
        {
            RefreshToken = refreshToken;
            Expire = expire;
        }

        public string RefreshToken { get; set; }
        public DateTime Expire { get; set; }
    }
}