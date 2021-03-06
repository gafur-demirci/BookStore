using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.GenreOperation.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;
        // Mapper config. u yapılır iki nesneyi bağlamak için ( genres ile GenresViewModel den geleni)
        public readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            // IsActive i true olan genre ları Id ye göre sıralayarak getirir.(Geriye genre ye dönülür varsa.)
            var genre = _context.Genres.SingleOrDefault( x => x.IsActive && x.Id == GenreId );
            if(genre is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı!");
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}