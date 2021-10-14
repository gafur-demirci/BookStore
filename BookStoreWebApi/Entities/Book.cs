using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebApi.Entities
{
    public class Book
    {
        // id yi auto increament olacak şekilde göstermek (unique olarak artması için identity seçildi)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public Genre Genre { get; set; }
        public Author Author { get; set; }
        
    }
}