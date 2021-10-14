using System;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                // Personal Growth
                new Book{Title = "Lean Startup",GenreId = 1, AuthorId = 1,PageCount = 200,PublishDate = new DateTime(2001,06,12)},

                // Science Fiction
                new Book{Title = "Herland",GenreId = 2, AuthorId = 2,PageCount = 250,PublishDate = new DateTime(2010,05,23)},
                
                // Science Fiction
                new Book{Title = "Dune",GenreId = 2, AuthorId = 3,PageCount = 540,PublishDate = new DateTime(2002,12,21)}
            );
        }
    }
}