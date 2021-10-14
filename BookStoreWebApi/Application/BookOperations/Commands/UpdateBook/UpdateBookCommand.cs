using System;
using System.Linq;
using BookStoreWebApi.Application.BookOperations.Queries.GetBooks;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _context;

        public int BookId {get; set;}

        public UpdateBookModel Model {get; set;}

        public UpdateBookCommand(IBookStoreDbContext Context)
        {
            _context = Context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault( x => x.Id == BookId);

            // eğer yoksa badrequest döner 
            if (book is null)
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");
            
            // varsa ilgili güncelleme verilerinin hangilerinin girildiği kontrol edilir.
            
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; 

            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            
            // verilen book nesnesinin genreıd isi defaulttan farklı mı yani değeri değiştirilmiş mi diye kontrol 
            // edilir değiştirilmiş ise o değiştirilmemiş ise default değer olarak bırakılır
            
            
            // db ile update yapıldığından kaydetme yapılmalıdır.
            _context.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}