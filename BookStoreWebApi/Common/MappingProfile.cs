using AutoMapper;
using BookStoreWebApi.Entities;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebApi.Application.BookOperations.Queries.GetBookDetail;
using BookStoreWebApi.Application.BookOperations.Queries.GetBooks;
using BookStoreWebApi.Application.GenreOperation.Queries.GetGenreDetail;
using BookStoreWebApi.Application.GenreOperation.Queries.GetGenres;
// post için
using static BookStoreWebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStoreWebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace BookStoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // BookMaps

            // GetBooks +
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            // Post işlemi için +
            CreateMap<CreateBookModel, Book>();  // <source , target> CreateBookModel objesi Book objesine maplenebilir olsun demektir.

            // GetById + 
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            // GenreMaps

            // GetGenres +
            CreateMap<Genre, GenresViewModel>();  // <source , target> Genre objesi GenresViewModes objesine maplenebilir olsun demektir.

            // GetGemreDetailQuery + 
            CreateMap<Genre, GenreDetailViewModel>();

            // Author Maps
            // GetBooksAuthor
            CreateMap<Author, BooksViewModel>().ForMember(dest => dest.Author, opt => opt.MapFrom(src =>src.book.Author));

            // GetAuthorsQuery
            CreateMap<Author , AuthorsViewModel>();

            // Post
            CreateMap<CreateAuthorModel, Author>();
            
            // GetAuthorDetailQuery
            CreateMap<Author , AuthorDetailViewModel>();

            // UserMaps

            // Post
            CreateMap<CreateUserModel, User>();
        }
    }
}