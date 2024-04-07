using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_web.Controllers
{
    public class PublisherController : Controller
    {

        private readonly ApplicationDbContext _db;
        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Publisher> publishers = await _db.Publishers.ToListAsync();

            return View(publishers);
        }

        public IActionResult Upsert(int id)
        {
            Publisher obj = new();

            if (id == 0 || id == null)
            {
                return View(obj);
            }

            obj = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upsert(Publisher publisher)
        {
            if(ModelState.IsValid)
            {
                if(publisher.Publisher_Id == 0)
                {
                    // create
                    await _db.Publishers.AddAsync(publisher);
                }
                else
                {
                    _db.Publishers.Update(publisher);
                }
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var obj =  _db.Publishers.FirstOrDefault(u => u.Publisher_Id == id);

           _db.Publishers.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}
