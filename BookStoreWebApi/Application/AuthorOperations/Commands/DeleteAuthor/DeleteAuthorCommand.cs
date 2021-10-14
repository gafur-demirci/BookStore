using System;
using System.Linq;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {

        private readonly IBookStoreDbContext _context;
        public int AuthorId {get; set;}
        public DateTime PublishDate {get; set;}

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault( x => x.Id == AuthorId);

            System.Console.WriteLine(author);

            if(author is null)
                throw new InvalidOperationException("Silinecek Yazar Bulunamadı!");

            /* _context.Authors.Remove(author);
            _context.SaveChanges(); */
        }
    }
}