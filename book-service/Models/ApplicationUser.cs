using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace book_service.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<BorrowBook> BorrowBooks{get;set;}
    }
}