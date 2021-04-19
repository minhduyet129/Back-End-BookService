using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace book_service.Models
{
    public class BookContext:IdentityDbContext<ApplicationUser>
    {
        public BookContext(){

        }
        public BookContext(DbContextOptions<BookContext> options) : base(options) {

        }
        public DbSet<Book> Books{get;set;}
        public DbSet<Category> Categories{get;set;}
        public DbSet<BorrowBook> BorrowBooks{get;set;}
        public DbSet<BorrowBookDetail> BorrowBookDetails{get;set;} 

        protected override void OnConfiguring(DbContextOptionsBuilder option){
            if (!option.IsConfigured){
                 option.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=BookService1;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
           
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}