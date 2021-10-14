using BookStoreWebApi.Application.GenreOperation.Queries.GetGenreDetail;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}