using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_service.Models
{
    public class BorrowBook
    {
        public int BorrowBookId{get;set;}
        public DateTime BorrowDate{get;set;}
        public string Status{get;set;}
       
       [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ICollection<BorrowBookDetail> BorrowBookDetails{get;set;}
        public ApplicationUser ApplicationUser{get;set;}

    }
}