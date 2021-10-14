using System;
using System.Linq;
using BookStoreWebApi.Application.GenreOperation.Commands.UpdateGenre;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;
using static BookStoreWebApi.Application.GenreOperation.Commands.UpdateGenre.UpdateGenreCommand;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNonExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 999;

            FluentActions.
                Invoking(() => command.Handle()).
                Should().
                Throw<InvalidOperationException>().
                And.Message.
                Should().
                Be("Güncellenecek Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 3;

            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "TestGenreName"
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.FirstOrDefault(genre => genre.Name == model.Name);

            genre.Should().NotBeNull();
        }
    }
}