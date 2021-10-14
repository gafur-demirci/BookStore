using BookStoreWebApi.Application.GenreOperation.Commands.UpdateGenre;
using FluentAssertions;
using TestSetup;
using Xunit;
using static BookStoreWebApi.Application.GenreOperation.Commands.UpdateGenre.UpdateGenreCommand;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "")]
        [InlineData(0, "tes")]
        [InlineData(-1, "")]
        [InlineData(-1, "tes")]
        [InlineData(null, "")]
        [InlineData(null, "tes")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = id;
            command.Model = new UpdateGenreModel()
            {
                Name = name
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel()
            {
                Name = "TestGenreName"
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }


        
    }
}