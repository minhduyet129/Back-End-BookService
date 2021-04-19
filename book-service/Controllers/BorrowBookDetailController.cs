using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace book_service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBookDetailController : ControllerBase
    {
        private readonly BookContext _context;
        public BorrowBookDetailController(BookContext context){
            _context=context;

        }
        [HttpGet("detailborrow")]
        public IEnumerable<BorrowBookDetail> GetListBorrowDetail(int borrowid){
            return _context.BorrowBookDetails.Where(x=>x.BorrowBookId==borrowid);
        }
    }
}