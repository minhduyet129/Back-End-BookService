using System.Collections.Generic;

namespace book_service.Models
{
    public class BorrowBookDetail
    {
        public int BorrowBookDetailId{get;set;}
        public int BorrowBookId{get;set;}
        public int BookId {get;set;}
        public int Quantity{get;set;}
        public BorrowBook BorrowBook{get;set;}
        public Book Book{get;set;}
    }
}