using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace book_service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BorrowBookController : ControllerBase
  {
    private readonly BookContext _context;
    public BorrowBookController(BookContext context){
      _context=context;
    }
    [Authorize(Roles=RoleName.Admin)]
    [HttpGet]
    public IEnumerable<BorrowBook> GetAllBorrowBook(){
      return _context.BorrowBooks.OrderByDescending(x=>x.BorrowDate);
    }

    [HttpGet("user")]
    public IEnumerable<BorrowBook> GetListBorrowBookUser(string userid){
      return _context.BorrowBooks.Where(x=>x.UserId==userid).OrderByDescending(x=>x.BorrowDate);
    }

    [Authorize(Roles=RoleName.Admin)]
    [HttpPost]
   public async Task<IActionResult> CreateBorrowBook(BorrowBook borrow)
   {

        var date=DateTime.Today.Month;
        var timesBorrow=_context.BorrowBooks.Where(x=>x.UserId==borrow.UserId && x.BorrowDate.Month==date );
        if(timesBorrow ==null){
          return BadRequest();
        }
        if(timesBorrow !=null){
          if(timesBorrow.Count()>2) return Ok("Ban da het lan muon trong thang");
        }
        
        var a= _context.BorrowBooks.Where(x => x.UserId == borrow.UserId && x.BorrowDate.Month == date);
        var items=0;
        foreach (var i in a){
          var val=_context.BorrowBookDetails.Where(x=>x.BorrowBookId==i.BorrowBookId).Sum(x=>x.Quantity);
          items +=val;
        }
        
        if(items >= 5) return BadRequest("Bạn đã hết số lần mượn trong tháng");

        if(borrow.BorrowBookDetails != null && borrow.BorrowBookDetails.Any()){
          var borrowed=borrow.BorrowBookDetails.Sum(x => x.Quantity);
          if(borrowed>5) return BadRequest("Ban chi duoc muon it hon 5 quyen");
          if((borrowed+items )> 5) return BadRequest("Ban chi dc muon it hon"+(5-borrowed));
          var newborrow=new BorrowBook{
              BorrowDate=borrow.BorrowDate,
              
              Status=borrow.Status,
              UserId=borrow.UserId
          };
           _context.BorrowBooks.Add(newborrow);
           _context.SaveChanges();
           var id=newborrow.BorrowBookId;

          foreach(var itemdetail in borrow.BorrowBookDetails){
            var detail =new BorrowBookDetail{
                BorrowBookId=id,
                BookId=itemdetail.BookId,
                Quantity=itemdetail.Quantity
            };
            _context.BorrowBookDetails.Add(detail);
          }
          await _context.SaveChangesAsync();
          return Ok("Secessed!");
        }
        return Ok("Danh sach muon trong");
    }

    [Authorize(Roles=RoleName.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBorrowBook(int id,BorrowBook borrowBook){
      if(id!=borrowBook.BorrowBookId){
        return BadRequest();
      }
      _context.Entry(borrowBook).State=EntityState.Modified;
      await _context.SaveChangesAsync();
      return Ok("Sucessed!");

    }
    
    [Authorize(Roles=RoleName.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrowBook(int id){
      var borrowBook= await _context.BorrowBooks.FirstAsync(x=>x.BorrowBookId==id);
      if(borrowBook==null){
        return NotFound();
      }
      _context.BorrowBooks.Remove(borrowBook);
      return Ok("Delete success");
    }
  }
  
  
}