using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Application.GenreOperation.Commands.CreateGenre;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;
using static BookStoreWebApi.Application.GenreOperation.Commands.CreateGenre.CreateGenreCommand;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "TestGenre"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = genre.Name
            };

            FluentActions.
                Invoking(() => command.Handle()).
                Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("Kitap Türü Zaten Mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);

            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "TestGenreNames"
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);

            genre.Should().NotBeNull();
        }
    }
}