using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentValidation;
using BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using static BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace BookStoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Tüm Author ları getirir.
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // Girilen id deki Author u getirir.
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            AuthorDetailViewModel result;

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        }

        // Yazar ekleme işlemi
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);

            command.Model = newAuthor;
            
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        // Yazar güncelleme işlemi(id)
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        // Yazar silme işlemi(id)
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();

            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}