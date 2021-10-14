using BookStoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}