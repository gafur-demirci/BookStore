using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int AuthorId {get; set;}

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(author => author.Id == AuthorId);

            if(author is null)
                throw new InvalidOperationException("Yazar Bulunamadı!");
            
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
            return vm;
        }

    }

    public class AuthorDetailViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
    }
    
}