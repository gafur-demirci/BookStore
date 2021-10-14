using System;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStoreWebApi.DBOperations;
using FluentAssertions;
using TestSetup;
using Xunit;
using static BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Fact]
        public void WhenInvalidAuthorIdIsGiven_Author_ShouldBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 999;

            FluentActions.
                Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.
                Should().
                Be("Güncellenecek Yazar Bulunamadı!");

        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;

            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FName = "TestAuthorFName",
                LName = "TestAuthorLName",
                BDate = DateTime.Now.Date.AddYears(-20)
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.FirstOrDefault(author => author.FName == model.FName && author.LName == model.LName);

            author.Should().NotBeNull();
            author.BDate.Should().Be(model.BDate);
        }
    }
}