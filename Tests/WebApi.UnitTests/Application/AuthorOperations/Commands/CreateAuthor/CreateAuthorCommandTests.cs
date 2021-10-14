using System;
using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using static BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;
using TestSetup;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            // Arrange

            var author = new Author()
            {
                FName = "TestAuthorFNames",
                LName = "TestAuthorLNames",
                BDate = DateTime.Now.Date.AddYears(-10),
                book = new Book()
                {
                     Title = "testAuthorBook"
                }
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel()
            {
                FName = author.FName,
                LName = author.LName
            };

            // Act-Assert

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazar Zaten Mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);

            CreateAuthorModel model = new CreateAuthorModel()
            {
                FName = "ValidAuthorFName",
                LName = "ValidAuthorLName",
                BDate = DateTime.Now.Date.AddYears(-50)
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

             var author = _context.Authors.SingleOrDefault(author => author.FName == model.FName && author.LName == model.LName);

             author.Should().NotBeNull();
             author.FName.Should().Be(model.FName);
             author.LName.Should().Be(model.LName);
             author.BDate.Should().Be(model.BDate);

        } 
    }
}