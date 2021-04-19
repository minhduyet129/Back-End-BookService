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
  public class CategoryController : ControllerBase
  {
   private readonly BookContext _context;
   public CategoryController(BookContext context){
     _context=context;
   }
   [HttpGet]
   public IEnumerable<Category> GetAllCategory(){
     return _context.Categories.OrderBy(x=>x.CategoryId);
   }
   [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id){
      var category =await _context.Categories.FindAsync(id);
      if(category==null){
        return NotFound();
      }
      return category;
    }
    [Authorize(Roles=RoleName.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category){
      _context.Categories.Add(category);
      await _context.SaveChangesAsync();
      return Ok(category);
    }
    [Authorize(Roles=RoleName.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id,Category category){
      if(id!=category.CategoryId){
        return BadRequest();
      }
      _context.Entry(category).State=EntityState.Modified;
      await _context.SaveChangesAsync();
      return Ok("Successed!");

    }
    [Authorize(Roles=RoleName.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id){
      var category  =await _context.Categories.FirstAsync(x=>x.CategoryId==id);
      if(category ==null){
        return NotFound();
      }
      _context.Categories.Remove(category);
      await _context.SaveChangesAsync();
      return Ok("Successed!");
    }
  }
}
