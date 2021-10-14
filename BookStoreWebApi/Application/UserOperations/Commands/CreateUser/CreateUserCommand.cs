using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(user => user.Email == Model.Email);
            if(user is not null)
                throw new InvalidOperationException("Kullanıcı Zaten Mevcut!");

            // eğer null ise mapper.map bana bir user objesi ver ve bunu modelden maple(bu ilişkiyi mappingprofile içinde belirtiyoruz)
            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();

        }

        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

    }
}