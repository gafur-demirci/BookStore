using FluentValidation;

namespace BookStoreWebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
            RuleFor(command => command.Model.Email).NotEmpty();
        }
    }
}