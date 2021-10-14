using FluentValidation;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.FName).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.LName).NotEmpty().MinimumLength(4);

            /* RuleFor(command => command.Model.BDate.Date).NotEqual(command => command.) */
        }
    }
}