using Xunit;
using TestSetup;
using BookStoreWebApi.Application.BookOperations.Commands.CreateBook;
using static BookStoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using System;
using FluentAssertions;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>  // Böylece CommonTestFixture da config edilen context ve mapper a erişimi sağlanır.
    {
        // Testlerin yazımı

        // Zaten var olan kitap adı verildiğinde geçersiz işlem hatası dönder (kitap zaten mevcut hata kısmı)
        //[Fact]
        // Theroy ile datanın modellerini inline data olarak vererek çoklu durum için test edebiliriz.
        // without DateTime tests
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]           // pageCount büyük 0, genreId büyük 0 olmalı
        [InlineData("Lord Of The Rings", 0, 1)]           // pageCount büyük 0 olmalı
        [InlineData("Lord Of The Rings", 100, 0)]         // genreId büyük 0 olmalı
        [InlineData("", 0, 0)]                            // title boş olmamalı, pageCount büyük 0, genreId büyük 0 olmalı
        [InlineData("", 100, 1)]                          // title boş olmamalı
        [InlineData("", 0, 1)]                            // title boş olmamalı, pageCount büyük 0 olmalı
        [InlineData("Lor", 100, 1)]                       // title min 4 char olmalı
        [InlineData("Lord", 100, 0)]                      // genreId büyük 0 olmalı
        [InlineData("Lord", 0, 1)]                        // pageCount büyük 0 olmalı
        [InlineData(" ", 100, 1)]                         // title empty string olmamalı,  

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            // Arrange (hazırlık)

            // test edilecek command örneği oluşturuluyor. İçeriğindeki context ve mapper kullanılmayacak sadece
            // modelin değerleri test edilecek o yüzden null gönderildi.

            CreateBookCommand command = new CreateBookCommand(null, null);

            // Validator ın kontrol ettiği değerler model aracılığı ile setlendi.(hatalı olarak)

            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),  // bir yıl öncesi olarak alır böylece publish date valid olur.
                GenreId = genreId
            };

            // Act (uygulama)

            // validator objesi oluşturuldu.

            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            // oluşturulan command objesi validator ile validate edilsin denildi.

            var result = validator.Validate(command);

            // Assert (doğrulama)

            // eğer validator validate ettiğinde hatalı durum bulursa bunu result ile tutacak. 
            // Hatalı durumun doğruluğu da bu kısımda denetleniyor.
            result.Errors.Count.Should().BeGreaterThan(0);
            // hatalı prop olmazsa test geçmez çünkü hata sayısı 0 dan büyük olmalı olarak söyledik.
            // buradaki testler title, pageCount ve genreId içindi publishDate için ayrıca test yazılacak.
        }
        // DateTime tests
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            // Arrange (hazırlık)

            CreateBookCommand command = new CreateBookCommand(null, null);

            command.Model = new CreateBookModel()
            {
                Title = "The Lord Of The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date,  // invalide date verildi
                GenreId = 1
            };

            // Act (uygulama)

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert (doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange (hazırlık)

            CreateBookCommand command = new CreateBookCommand(null, null);

            command.Model = new CreateBookModel()
            {
                Title = "The Lord Of The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-2),  // valid date verildi
                GenreId = 1
            };

            // Act (uygulama)

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // Assert (doğrulama)
            result.Errors.Count.Should().Equals(0);  // Be(0) da denebilirdi. Hata sayısının 0 olduğunu söylüyoruz(tüm proplar valid verildi)
        }
    }
}