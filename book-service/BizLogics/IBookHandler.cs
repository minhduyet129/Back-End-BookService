using System.Threading.Tasks;
using System.Collections.Generic;
using book_service.Models;

namespace book_service.BizLogics
{
    public interface IBookHandler
    {
        IEnumerable<Book> GetListBook();
        Book GetBookById(int id);
        
    }
}