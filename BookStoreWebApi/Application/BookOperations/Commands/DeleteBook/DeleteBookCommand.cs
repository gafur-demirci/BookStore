using System;
using System.Linq;
using BookStoreWebApi.Application.BookOperations.Queries.GetBooks;
using BookStoreWebApi.Common;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId {get; set;}

        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault( x => x.Id == BookId);

            if ( book is null)
                throw new InvalidOperationException("Silinecek Kitap Bulunamadı!");
            
            // static ile kaldırma
            // BookList.Remove(book);
            
            // db ile remove etme ve sonrasında kaydetme
            
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

    }
}