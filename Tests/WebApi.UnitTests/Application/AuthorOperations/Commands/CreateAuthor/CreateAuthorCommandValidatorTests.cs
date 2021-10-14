using System;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using TestSetup;
using Xunit;
using static BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" ", " ")]
        [InlineData("", "test")] 
        [InlineData("", "TestLName")]
        [InlineData("TestFName", "")]
        [InlineData("TestFName", "test")]
        [InlineData("te", "")]
        [InlineData("te", "test")]
        [InlineData("te", "TestLName")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string fName, string lName)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);

            command.Model = new CreateAuthorModel()
            {
                FName = fName,
                LName = lName,
                BDate = DateTime.Now.Date.AddYears(-20)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);

            command.Model = new CreateAuthorModel()
            {
                FName = "TestAuthorFName",
                LName = "TestAuthorLName",
                BDate = DateTime.Now.Date
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);

            command.Model = new CreateAuthorModel()
            {
                FName = "TestAuthorFName",
                LName = "TestAuthorLName",
                BDate = DateTime.Now.Date.AddYears(-20)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }

}