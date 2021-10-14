using System;
using BookStoreWebApi.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using TestSetup;
using Xunit;
using static BookStoreWebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.Updatebook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "", 1, 0)]
        [InlineData(0, "Lor", 100, 1)]
        [InlineData(0, "Lord", 0, 1)]
        [InlineData(1, "", 1, 0)]
        [InlineData(1, "Lor", 100, 1)]
        [InlineData(1, "Lord", 0, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string title, int pageCount, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = id;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-5),
                GenreId = genreId
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;

            command.Model = new UpdateBookModel()
            {
                Title = "The Lord The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange (hazırlık)

            UpdateBookCommand command = new UpdateBookCommand(null);

            command.Model = new UpdateBookModel()
            {
                Title = "The Lord Of The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-2),  // valid date verildi
                GenreId = 1
            };

            // Act (uygulama)

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert (doğrulama)
            result.Errors.Count.Should().Equals(0);  // Be(0) da denebilirdi. Hata sayısının 0 olduğunu söylüyoruz(tüm proplar valid verildi)
        }
    }

}