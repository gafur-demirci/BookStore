using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebApi.Entities
{
    public class Author
    {
        // id yi auto increament olacak şekilde göstermek (unique olarak artması için identity seçildi)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime BDate { get; set; }
        public bool IsPublish { get; set; } = true;
        public Book book {get; set;}
    }
}