using BookStoreWebApi.Application.GenreOperation.Commands.CreateGenre;
using BookStoreWebApi.Application.GenreOperation.Commands.UpdateGenre;
using BookStoreWebApi.Application.GenreOperation.Commands.DeleteGenre;
using BookStoreWebApi.Application.GenreOperation.Queries.GetGenres;
using BookStoreWebApi.Application.GenreOperation.Queries.GetGenreDetail;
using static BookStoreWebApi.Application.GenreOperation.Commands.CreateGenre.CreateGenreCommand;
using static BookStoreWebApi.Application.GenreOperation.Commands.UpdateGenre.UpdateGenreCommand;
using BookStoreWebApi.DBOperations;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;

namespace BookStoreWebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Endpoints
        // Tüm genre ları getirir. (parametresiz kullanılır)
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);

            var result = query.Handle();

            return Ok(result);
        }

        // id ile genre getirir. (parametresi id)
        [HttpGet("{id}")]

        public IActionResult GetGenreDetail(int id)
        {
            GenreDetailViewModel result;

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]

        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updatedGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}