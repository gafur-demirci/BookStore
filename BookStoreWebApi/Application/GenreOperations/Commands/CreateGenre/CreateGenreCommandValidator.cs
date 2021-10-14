using FluentValidation;

namespace BookStoreWebApi.Application.GenreOperation.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor( command => command.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}