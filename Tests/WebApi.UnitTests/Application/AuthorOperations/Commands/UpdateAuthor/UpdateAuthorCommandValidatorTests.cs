using System;
using BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"","")]
        [InlineData(0,"","LName")]
        [InlineData(0,"FName","")]
        [InlineData(1,"","LName")]
        [InlineData(1,"FName","")]
        [InlineData(1,"","")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string fName, string lName)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = id;
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel()
            {
                FName = fName,
                LName = lName
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;

            command.Model = new UpdateAuthorCommand.UpdateAuthorModel()
            {
                FName = "TestAuthorFName",
                LName = "TestAuthorLName",
                BDate = DateTime.Now.Date
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel()
            {
                FName = "TestAuthorFName",
                LName = "TestAuthorLName",
                BDate = DateTime.Now.Date.AddYears(-20)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}