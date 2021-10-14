using System;
using BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNonExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 999;

            // Act-Assert
            FluentActions.
                Invoking(() => command.Handle()).
                Should().
                Throw<InvalidOperationException>().
                And.
                Message.
                Should().
                Be("Silinecek Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeDeleted()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;

            FluentActions.
                Invoking(() => command.Handle()).
                Should().
                NotThrow();
        }
    }
}