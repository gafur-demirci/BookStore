using System;
using BookStoreWebApi.Application.BookOperations.Commands.DeleteBook;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            // Arrange

/*             var book = new Book()
            {
                Title = "Tests",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10)
            };
            
            _context.Books.Add(book);
            _context.Books.Remove(book);
            _context.SaveChanges(); */

            DeleteBookCommand command = new DeleteBookCommand(_context);
            // command.BookId = book.Id;
            command.BookId = 999;

            // Act-Assert

            FluentActions.
                Invoking(() => command.Handle()).
                Should().
                Throw<InvalidOperationException>().
                And.Message.
                Should().
                Be("Silinecek Kitap Bulunamadı!");

        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            // Arrange

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;

            // Act-Assert

            FluentActions.
                Invoking(() => command.Handle()).
                Should().
                NotThrow();

        }
    }
}