using Library.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly AppDbContext _db;

        public BookController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var DbBook = await _db.Book.FirstOrDefaultAsync(book => book.Id == id);
            if(DbBook == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Remove(DbBook);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successfull!" });
        }
    }
}
