using System;
using System.Linq;
using AutoMapper;

using BookStoreWebApi.DBOperations;
using BookStoreWebApi.TokenOperations;
using BookStoreWebApi.TokenOperations.Models;
using Microsoft.Extensions.Configuration;


namespace BookStoreWebApi.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model {get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            // token üretilmesi istenen kullanıcının kayıtlı olup olmadığı kontrol edilir.
            var user = _context.Users.FirstOrDefault(user => user.Email == Model.Email && user.Password == Model.Password);
            // user varsa
            if(user is not null)
            {
                // token oluştur
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();
                return token;

            }else
                throw new InvalidOperationException("Kullanıcı Adı - Sifre Hatalı!");
        }

        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

    }
}