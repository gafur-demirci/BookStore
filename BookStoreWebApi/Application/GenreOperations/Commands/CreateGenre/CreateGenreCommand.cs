using System;
using System.Linq;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Application.GenreOperation.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model {get; set;}
        private readonly IBookStoreDbContext _context;

        public CreateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if(genre is not null)
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut!");
            // eğer verilen name da genre yoksa
            genre = new Genre();
            // Boş örneğine eşitle
            genre.Name = Model.Name;
            // dışarıdan girilen name i boş nesneye atayarak setter işlemi yap
            _context.Genres.Add(genre);
            // bu nesneyi db ye ekle
            _context.SaveChanges();
            // yapılan değişiklikleri kaydet
        }
        public class CreateGenreModel
        {
            public string Name { get; set; }
        }
            
    }

}