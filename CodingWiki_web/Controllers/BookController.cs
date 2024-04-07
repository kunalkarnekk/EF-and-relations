using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using CodingWiki_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace CodingWiki_web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<Book> ObjList =  _db.Books.Include(u => u.Publisher)
                .Include(u => u.BookAuthorMap).ThenInclude(u => u.Author);

            var temp = ObjList.Where(u => u.BookId == 1).ToList();

            //List<Book> books = await _db.Books.ToListAsync();
            //foreach (var obj in books)
            //{
            //    //least effeicnet
            //    //obj.publisher = _db.publishers.find(obj.publisher_id);

            //    //more effeicent
            //    _db.Entry(obj).Reference(u => u.Publisher).Load();
            //    _db.Entry(obj).Collection(u => u.BookAuthorMap).Load();
            //    foreach (var bookAuth in obj.BookAuthorMap )
            //    {
            //        _db.Entry(bookAuth).Reference(u => u.Author).Load();
            //    }

            //}
            return View(ObjList);
        }

        public IActionResult Upsert(int? id)
        {
             BookVM  obj = new();

            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });

            if(id == null || id == 0)
            {
                return View(obj);
            }

            //edit
            obj.Book = _db.Books.FirstOrDefault(u => u.BookId == id);

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Upsert(BookVM obj)
        {
            if(obj.Book.BookId == 0)
            {
                //create
                await _db.Books.AddAsync(obj.Book);
            }
            else
            {
                //Update 
                _db.Books.Update(obj.Book);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         
        }

        public IActionResult Details(int? id)
        {
           

            if (id == null || id == 0)
            {
                return NotFound();
            }

            BookDetail obj = new();


            //edit
            obj.Book = _db.Books.FirstOrDefault(u => u.BookId == id);
            obj = _db.BookDetails.Include(u => u.Book).FirstOrDefault(u => u.Book_Id == id);


            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Details(BookDetail obj)
        {
            if (obj.BookDetail_Id == 0)
            {
                //create
                await _db.BookDetails.AddAsync(obj);
            }
            else
            {
                //Update 
                _db.BookDetails.Update(obj);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if(Id == 0 || Id == null)
            {
                return NotFound();
            }

            var obj = _db.Books.FirstOrDefault(u => u.BookId == Id);

            if(obj == null)
            {
                return NotFound();
            }

            _db.Books.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ManageAuthors(int id )
        {
            BookAuthorVM obj = new()
            {
                BookAuthorList = _db.BookAuthorMaps.Include(u => u.Author).Include(u => u.Book)
                        .Where(u => u.Book_Id == id).ToList(),
                BookAuthor = new()
                {
                    Book_Id = id
                },

                Book = _db.Books.FirstOrDefault(u => u.BookId == id)

            };

            List<int> tempListOfAssignedAuthor = obj.BookAuthorList.Select(u => u.Author_Id).ToList();

            //Not In Clause
            //Get all the authors whose id is not in tempListOfAssignedAuthors

            var tempList = _db.Authors.Where( u=> !tempListOfAssignedAuthor.Contains(u.Author_Id)).ToList();
            obj.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });

            return View(obj);
            
        }


        [HttpPost]

        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if(bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthorMaps.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        [HttpPost]

        public IActionResult RemoveAuthors( int authorId , BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.BookId;

            BookAuthorMap bookAuthorMap = _db.BookAuthorMaps.FirstOrDefault(
                u => u.Author_Id == authorId && u.Book_Id == bookId);

            _db.BookAuthorMaps.Remove(bookAuthorMap);
            _db.SaveChanges();

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

        public async Task<IActionResult> PlayGround()
        {

            IEnumerable<Book> BookList = _db.Books;
            var FilteredBook = BookList.Where(b => b.Price > 50).ToList();

            IQueryable<Book> BookList2 = _db.Books;
            var FilteredBook2 = BookList2.Where(b => b.Price > 50).ToList();

            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //decimal totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _db.Books.Count();
            return RedirectToAction(nameof(Index));
        }
    }
}
