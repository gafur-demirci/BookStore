using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BookStoreWebApi.Entities;
using BookStoreWebApi.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreWebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            // boş token objesi oluşturulur daha sonra configleri verilir.
            Token tokenModel = new Token();

            // kullanılacak key in ne olduğu belirtilir.
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            // oluşturulan(appSettings.json daki) SecurityKey i verilen algoritma ile şifreler
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 15 dk lık kullanım süresi olduğunu belirtir.
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            //
            JwtSecurityToken securityToken = new JwtSecurityToken
            (
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // token yaratılıyor
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();
            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}