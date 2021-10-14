using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.DBOperations
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        // genre içeriğini entities içindeki genre den almak için db içeriğine bildirme işlemi(datageneratorda nesnesi oluşturulur.)
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            // DbContext deki SaveChanges methodunu çağırmasını istedik.Başka şeyler de isteyebilirdik.
            return base.SaveChanges();
        }
    }
}