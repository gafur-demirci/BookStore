using System;
using FluentValidation;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {

            RuleFor(command => command.Model.FName).NotEmpty().MinimumLength(3);

            RuleFor(command => command.Model.LName).NotEmpty().MinimumLength(5);

            RuleFor(command => command.Model.BDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}