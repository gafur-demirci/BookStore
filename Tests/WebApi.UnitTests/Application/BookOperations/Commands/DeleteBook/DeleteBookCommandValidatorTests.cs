using BookStoreWebApi.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnError(int id)
        {
            // Arrange

            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            // Act

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;

            // Act

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // Assert

            result.Errors.Count.Should().Equals(0);
            
        }
    }
}