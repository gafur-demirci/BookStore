using FluentValidation;

namespace BookStoreWebApi.Application.GenreOperation.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator: AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}