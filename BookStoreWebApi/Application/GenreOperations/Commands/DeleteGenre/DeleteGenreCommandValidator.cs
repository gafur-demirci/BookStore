using FluentValidation;

namespace BookStoreWebApi.Application.GenreOperation.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor( command => command.GenreId).GreaterThan(0);
        }
    }
}