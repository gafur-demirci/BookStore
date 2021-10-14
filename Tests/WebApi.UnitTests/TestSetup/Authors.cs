using System;
using BookStoreWebApi.DBOperations;
using BookStoreWebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author{FName = "Ali",LName = "Yazgeldi",BDate = new DateTime(1990,12,05)},

                new Author{FName = "Zeynep",LName = "Solmaz",BDate = new DateTime(1980,12,05)},
                
                new Author{FName = "Fatih",LName = "Çelik",BDate = new DateTime(1996,12,05)}
            );
        }
    }
}