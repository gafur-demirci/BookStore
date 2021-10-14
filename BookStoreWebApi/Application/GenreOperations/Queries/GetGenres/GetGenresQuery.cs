using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.GenreOperation.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _context;
        // Mapper config. u yapılır iki nesneyi bağlamak için ( genres ile GenresViewModel den geleni)
        public readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            // IsActive i true olan genre ları Id ye göre sıralayarak getirir.(Geri dönüş modeli yaratılır.)
            var genres = _context.Genres.Where( x => x.IsActive ).OrderBy( x => x.Id );
            // Geri dönüş obj si
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}