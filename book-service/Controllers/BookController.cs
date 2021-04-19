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
  public class BookController : ControllerBase
  {
    private readonly BookContext _context;
    public BookController(BookContext context){
      _context =context;
    }
    [HttpGet]
    public IEnumerable<Book> GetAllBook(){
      return _context.Books.OrderBy(x=>x.BookId);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id){
      var book =await _context.Books.FindAsync(id);
      if(book==null){
        return NotFound();
      }
      return book;
    }
    [Authorize(Roles=RoleName.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateBook(Book book){
      _context.Books.Add(book);
      await _context.SaveChangesAsync();
      return Ok(book);
    }
    [Authorize(Roles=RoleName.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id,Book book){
      if(id!=book.BookId){
        return BadRequest();
      }
      _context.Entry(book).State=EntityState.Modified;
      await _context.SaveChangesAsync();
      return Ok("Sucessed!");

    }
    [Authorize(Roles=RoleName.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id){
      var book =await _context.Books.FirstAsync(x=>x.BookId==id);
      if(book ==null){
        return NotFound();
      }
      _context.Books.Remove(book);
      await _context.SaveChangesAsync();
      return Ok("Successed!");
    }
  }
}