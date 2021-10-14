using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.DBOperations
{
    public interface IBookStoreDbContext
    {
        // Dışarıdan alınan objectlerin sample ı oluşturulur.
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<User> Users { get; set; }
        // DbContext de bulunan SaveChanges methodu
        int SaveChanges();

    }
}