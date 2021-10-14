using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault( x => x.FName == Model.FName && x.LName == Model.LName);

            if(author is not null)
                throw new InvalidOperationException("Yazar Zaten Mevcut!");
            
            author = _mapper.Map<Author>(Model);

            _context.Authors.Add(author);
            _context.SaveChanges();

        }

        public class CreateAuthorModel
        {
            public string FName { get; set; }
            public string LName { get; set; }
            public DateTime BDate { get; set; }
        }
    }
}