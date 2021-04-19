using System;
using System.Collections.Generic;

namespace book_service.Models
{
    public class Category
    {
        public int CategoryId{get;set;}
        public string CategoryName{get;set;}
        public ICollection<Book> Books{get;set;}
    }
}