using System;
using BookStoreWebApi.Application.GenreOperation.Commands.DeleteGenre;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNonExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 999;

            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            FluentActions.
                Invoking(() => command.Handle()).
                Should().
                NotThrow();
        }
    }
}