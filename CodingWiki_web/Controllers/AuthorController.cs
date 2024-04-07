using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_web.Controllers
{
    public class AuthorController : Controller
    {

        private readonly ApplicationDbContext _db;
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _db.Authors.ToListAsync();
            return View(authors);
        }

        public IActionResult Upsert(int? id)
        {
            Author obj = new();

           
                if (id == 0 || id == null)
                {
                    return View(obj);
                }
                obj = _db.Authors.FirstOrDefault(u => u.Author_Id == id);
                if(obj == null)
                {
                    return NotFound();
                }
            
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert( Author obj )
        {
            if (ModelState.IsValid)
            {
                if(obj.Author_Id == 0)
                {
                    await _db.Authors.AddAsync(obj);
                }
                else
                {
                    _db.Authors.Update(obj);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }

            return View(obj);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var obj = _db.Authors.FirstOrDefault(u => u.Author_Id == id);

            if(obj == null)
            {
                return NotFound();
            }

            _db.Authors.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
