 using System;
using System.Linq;
using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreWebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Noval"
                    }
                );

                // id yi auto increament yaptığımız için yoruma aldık
                context.Books.AddRange(
                    new Book{
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1, // Personal Growth
                        AuthorId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12)
                    },

                    new Book{
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2, // Science Fiction
                        AuthorId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010,05,23)
                    },

                    new Book{
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2, // Science Fiction
                        AuthorId = 3,
                        PageCount = 540,
                        PublishDate = new DateTime(2002,12,21)
                    }
                );

                context.Authors.AddRange(
                    new Author{
                        FName = "Ali",
                        LName = "Yazgeldi",
                        BDate = new DateTime(1990,12,05)
                    },
                    new Author{
                        FName = "Zeynep",
                        LName = "Solmaz",
                        BDate = new DateTime(1980,12,05)
                    },
                    new Author{
                        FName = "Fatih",
                        LName = "Çelik",
                        BDate = new DateTime(1996,12,05)
                    }
                );
                context.SaveChanges();
            }

        }
    }
}