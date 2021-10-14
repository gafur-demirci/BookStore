using System;
using System.Linq;
using BookStoreWebApi.DBOperations;

namespace BookStoreWebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId {get; set;}

        public UpdateAuthorModel Model {get; set;}

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault( x => x.Id == AuthorId);

            if(author is null)
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!");
            
            author.FName = Model.FName != default ? Model.FName : author.FName;

            author.LName = Model.LName != default ? Model.LName : author.LName;

            author.BDate = Model.BDate.Date != default ? Model.BDate.Date : author.BDate.Date;

            _context.SaveChanges(); 

        }

        public class UpdateAuthorModel
        {
            public string FName { get; set; }
            public string LName { get; set; }
            public DateTime BDate { get; set; }
        }
    }
}