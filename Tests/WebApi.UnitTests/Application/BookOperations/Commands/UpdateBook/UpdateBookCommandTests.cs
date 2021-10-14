using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Application.BookOperations.Commands.UpdateBook;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;
using static BookStoreWebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.Updatebook
{    
    public class UpdateBookCommandTests :IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenNonExistBookIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 999;

            FluentActions.
                Invoking(() => command.Handle()).
                Should().
                Throw<InvalidOperationException>().
                And.Message.
                Should().
                Be("Güncellenecek Kitap Bulunamadı!");

        }

        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 3;

            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Lord Of The Rings Return Of The King",
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-5)
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.FirstOrDefault(book => book.Title == model.Title);

            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
        }
    }
}