using System;
using FluentValidation;

namespace BookStoreWebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            // Model'in GenreId prop u 0 dan büyük olmalıdır kuralı
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            // Model'in PageCount prop u 0 dan büyük olmalıdır kuralı
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            // Model'in PublishDate prop unun Date i yani tarih kısmı boş olamaz ve bugünden daha önce olmalı (geleceğe ait gün verilmemeli)
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            // Model'in Title prop u boş olmamalıdır ve uzunluğu 4 ve üstü olmalıdır kuralı
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);

        }
    }
}