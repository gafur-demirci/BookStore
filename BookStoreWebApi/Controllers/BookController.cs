using BookStoreWebApi.Application.BookOperations.Commands.CreateBook;   
using BookStoreWebApi.Application.BookOperations.Commands.UpdateBook;
using BookStoreWebApi.Application.BookOperations.Commands.DeleteBook;
using BookStoreWebApi.Application.BookOperations.Queries.GetBooks;
using BookStoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using static BookStoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static BookStoreWebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using BookStoreWebApi.DBOperations;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreWebApi.Controllers
{
    // token ile koruma
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        
        private readonly IBookStoreDbContext _context;
        // private sadece buradan erişim olması için readonly sadece const. içinde değiştirilsin sebebiyle verildi.
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
        static db için uygulanan yöntem 
        private static List<Book> BookList = new List<Book>(){
        
            new Book{
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1, // Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12)
            },
        
            new Book{
                Id = 2,
                Title = "Herland",
                GenreId = 2, // Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010,05,23)
            },
        
            new Book{
                Id = 3,
                Title = "Dune",
                GenreId = 2, // Science Fiction
                PageCount = 540,
                PublishDate = new DateTime(2002,12,21)
            }
        
        };
        */

        // iki get aynı anda olmayacağından biri yoruma alındı :)
        [HttpGet]
        public IActionResult GetBooks()
        {
            // geriye 200 ok bilgisi ve nesneyi dönmek için List<Book> yerine IActionResult olarak tanımlanır fonk.
            
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
            
            // staticte kullanım ile kitap listesi döndürme
            // var bookList = BookList.OrderBy(x => x.Id)) bookList.Add(book);
            
            /*
            // db den kitap listesi döndürme
            // BookOperations dan gelene göre düzenlenir
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList; 
            */
            
        }
        
        // Route dan id alma ile sorgulama
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;

            /*             
                try
            { */
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
                
            /* 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } */

            return Ok(result);

            // staticte kullanım ile  id ye göre kitap döndürme
            // var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            
            // db den id ile kitap döndürme
            /* var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book; */
        }

        // from query den id alma ile sorgulama
        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        // Post işlemi (bir/birkaç kitap ekleme işlemi)
        
        [HttpPost]
        public IActionResult AddBook ([FromBody] CreateBookModel newBook)
        {
            
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            /*             
            try
            { */
            // model maplendikten sonra validator kullanılmalı ardından handle çalıştırılmalıdır.
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            // ValidationResult result = validator.Validate(command);
            validator.ValidateAndThrow(command);
            command.Handle();

                /*
                // result hata dönerse bu hatayı konsolda göster yoksa handle ı çalıştır kısmı bunun yerine ekleme işlemini geçersiz kılmayı sağlayacağız.
                if(!result.IsValid)
                    foreach (var item in result.Errors)
                    {
                        Console.WriteLine("Property: " + item.PropertyName + " - Error Message: " + item.ErrorMessage);
                    }
                else
                {
                    command.Handle();
                } */

            /*
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } */
            
            return Ok();
            /*
            // db de eklenmek istenen öge kontrol işlemi (title a göre)
            // var book = BookList.SingleOrDefault( x => x.Title == newBook.Title);
            
            // db ile olan versiyon
            var book = _context.Books.SingleOrDefault( x => x.Title == newBook.Title);

            if(book is not null)
                return BadRequest();  // eğer eklenmek istenen varsa badraquest ile hatalı işlem olduğunu yoksa da ok ile işlemi gerçekleştireceğini belirtiyoruz. Bunun için post da geri dönüş gerekli değildir ama sorgulama yapıdığı için geri dönüş gerektiğinden IActionResult olarak dönülür

            // BookList.Add(newBook);
            
            // db ile olan versiyon ( ekledikten sonra kaydetme işlemi yapılır db kullanıldığı için )
            _context.Books.Add(newBook);
            _context.SaveChanges();
            */
        }

        // Put işlemi (id ye göre (route dan alınan))
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
/*             try
            { */
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
                
/*             }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } */

            return Ok();

            /* // değiştirilmek istenen kitap id ye sahip ögenin varlığı kontrol edilir.
            // var book = BookList.SingleOrDefault( x => x.Id == id);
            // db ile versiyonu
            var book = _context.Books.SingleOrDefault( x => x.Id == id);

            // eğer yoksa badrequest döner 
            if (book is null)
                return BadRequest();
            
            // varsa ilgili güncelleme verilerinin hangilerinin girildiği kontrol edilir.
            
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId; 
            
            // verilen book nesnesinin genreıd isi defaulttan farklı mı yani değeri değiştirilmiş mi diye kontrol 
            // edilir değiştirilmiş ise o değiştirilmemiş ise default değer olarak bırakılır
            
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            
            // db ile update yapıldığından kaydetme yapılmalıdır. */
        }

        // Delete işlemi (id ye göre (route dan alınan))
        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
/*             try
            { */
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle(); 
/*             }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } */

            return Ok();
            
            /* // static ile kitap kontrolü
            // var book = BookList.SingleOrDefault( x => x.Id == id);
            // db ile olan versiyonu
            
            var book = _context.Books.SingleOrDefault( x => x.Id == id);

            if ( book is null)
                return BadRequest();
            
            // static ile kaldırma
            // BookList.Remove(book);
            
            // db ile remove etme ve sonrasında kaydetme
            
            _context.Books.Remove(book);
            _context.SaveChanges(); */

        }
    }
}