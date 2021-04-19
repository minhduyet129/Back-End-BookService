using System.Collections.Generic;

namespace book_service.Models
{
    public class Book
    {
        public int BookId{get;set;}
        public string BookName{get;set;}
        public int CategoryId{get;set;}
        public Category category{get;set;}
        
        public ICollection<BorrowBookDetail> BorrowBookDetails{get;set;}

    }
}