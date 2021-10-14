using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            // db de eklenmek istenen öge kontrol işlemi (title a göre)
            // var book = BookList.SingleOrDefault( x => x.Title == newBook.Title);
            
            // db ile olan versiyon
            var book = _dbContext.Books.SingleOrDefault( x => x.Title == Model.Title);

            if (book is not null)
                throw new InvalidOperationException("Kitap Zaten Mevcut!");  // eğer eklenmek istenen varsa 
            // Bunun için post da geri dönüş gerekli değildir ama sorgulama yapıdığı için geri dönüş gerektiğinden IActionResult olarak dönülür

            book = _mapper.Map<Book>(Model);    //new Book();  Model ile gelen veriyi Book objesine maple demektir.

            /*          
            // Auto mapper ile dönüştürme yapıldığı için gerek kalmadı(MappingProfile.cs de)
            book.Title = Model.Title;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;
            book.PublishDate = Model.PublishDate; 
            */
            
            
            // BookList.Add(newBook);
            
            // db ile olan versiyon ( ekledikten sonra kaydetme işlemi yapılır db kullanıldığı için )
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();    
        }
        // Book objesi CreateBookModel e dönüştürülebilir olması için mapleme yapılabilir.
        // Kitap Eklerken dışarıdan alınacakları içeren sınıf
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}