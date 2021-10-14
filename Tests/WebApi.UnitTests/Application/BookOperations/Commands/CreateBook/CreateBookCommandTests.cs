using Xunit;
using TestSetup;
using BookStoreWebApi.DBOperations;
using AutoMapper;
using BookStoreWebApi.Entities;
using System;
using BookStoreWebApi.Application.BookOperations.Commands.CreateBook;
using static BookStoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using FluentAssertions;
using System.Linq;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>  // Böylece CommonTestFixture da config edilen context ve mapper a erişimi sağlanır.
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            // CommonTestFixture da yapılan configleri eşitleme işlemi
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        // Testlerin yazımı

        // Zaten var olan kitap adı verildiğinde geçersiz işlem hatası dönder (kitap zaten mevcut hata kısmı)
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (hazırlık)
            // test edilecek kitap örneği database dekiler dışında kullanılacağı için oluşturuluyor.

            var book = new Book()
            {
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationExepsiton_ShouldBeReturn", GenreId = 1, PageCount = 100, PublishDate = new DateTime(1990,01,12),AuthorId = 1
            };
            // db ye kaydetme
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title = book.Title};

            // Act (uygulama)
            // Assert (doğrulama)

            // command ın Handle methodunu ayağa kaldır ve mesajı .. olan bir exep döndürüyor mu test et ve doğrula
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // Arrange (hazırlık)

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);

            CreateBookModel model = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 2,
                AuthorId = 1
            };
            command.Model = model;

            // Act (uygulama)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Act ile assert ayrı olduğunda should asserte ait bu yüzden Invoking ile verilen methodun çalışması için Invoke methodu kullanılır.
            // (Act ve assert birlikte olsaydı should be ile method Invoke edilirdi.)

            // Assert (doğrulama)
            // arrange de oluşturulan book nesnesinin title ı na ait bir book oluştu mu diye soruyoruz. (sonucun null olmaması lazım eklenmiş durumda)
            
            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);

            // kontrol edilen parametreler (eşitliği)

            book.Should().NotBeNull();                          // kitap nesnesi null olmamalı yani nesnenin var olması lazım.
            book.PageCount.Should().Be(model.PageCount);        // kitap nesnesinin PageCount u modelin PageCount una eşit olmalı.
            book.PublishDate.Should().Be(model.PublishDate);    // kitap nesnesinin PublishDate i modelin PublishDate ine eşit olmalı.
            book.GenreId.Should().Be(model.GenreId);            // kitap nesnesinin GenreId si modelin GenreId sine eşit olmalı.
            book.AuthorId.Should().Be(model.AuthorId);          // kitap nesnesinin AuthorId si modelin AuthorId sine eşit olmalı.
        }
    }
}