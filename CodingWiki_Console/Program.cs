// See https://aka.ms/new-console-template for more information
using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Console.WriteLine("Hello, World!");


//using(ApplicationDbContext context = new())
//{
//    context.Database.EnsureCreated();

//    if(context.Database.GetPendingMigrations().Count() > 0)
//    {
//        context.Database.Migrate();
//    }
//}


//AddBook();
//GetAllBooks();
//GetBook();
//UpdateBook();
//DeleteBook();

// async void DeleteBook()
//{
//    using var context = new ApplicationDbContext();
//    var books = await context.Books.FindAsync(7);
//    context.Books.Remove(books);
//    await context.SaveChangesAsync();
//}

// async void UpdateBook()
//{
//    try
//    {
//        using var context = new ApplicationDbContext();
//        //var books = context.Books.Find(7);
//        //books.ISBN = "777";

//        var books = await context.Books.Where(u => u.Publisher_Id == 1).ToListAsync();
//        foreach (var book in books)
//        {
//            book.Price = 55.55m;
//        }

//       await  context.SaveChangesAsync();
        
//    }
//    catch(Exception ex)
//    {

//    }
//}

// async void GetBook()
//{
//    try
//    {
//    using var context = new ApplicationDbContext();

        //Book  book = context.Books.First();
        //var book = context.Books.FirstOrDefault();
        //Console.WriteLine(book.Title + " _ " + book.ISBN);

        //var books = context.Books.Where(u => u.Publisher_Id == 3);
        //foreach (var book in books)
        //{
        //    Console.WriteLine(book.Title + " - " + book.ISBN);
        //}

        //var book = context.Books.Where(u => u.Publisher_Id == 3 && u.Price > 30).FirstOrDefault();
        //Console.WriteLine(book.Title + " _ " + book.ISBN);


        //var input = "Cookie Jar"
        //var book = context.Books.FirstOrDefault(u => u.Title == input);
        //Console.WriteLine(book.Title + " _ " + book.ISBN);


        //var book = context.Books.Find(7);
        //var book = context.Books.Single(u => u.ISBN == "12312312");
        //Console.WriteLine(book.Title + " _ " + book.ISBN);

        //var books = context.Books.Where(u => u.ISBN.Contains("12"));
        //var books = context.Books.Where(u => EF.Functions.Like(u.ISBN, "12%"));
        //var books = context.Books.Where(u => EF.Functions.Like(u.ISBN, "12%")).Max(u => u.Price);
        //foreach (var book in books)
        //{
        //    Console.WriteLine(book.Title + " - " + book.ISBN);
        //}


        //var books = context.Books.Where(u => u.Price > 10).OrderBy(u => u.Title).ThenByDescending(u => u.ISBN);
        //foreach (var book in books)
        //{
        //    Console.WriteLine(book.Title + " - " + book.ISBN);
        //}

//        var books = await context.Books.Skip(0).Take(2).ToListAsync();
//        foreach (var book in books)
//        {
//            Console.WriteLine(book.Title + " - " + book.ISBN);
//        }

//        books =  await context.Books.Skip(4).Take(1).ToListAsync();
//        foreach (var book in books)
//        {
//            Console.WriteLine(book.Title + " - " + book.ISBN);
//        }




//    }
//    catch (Exception ex)
//    {

//    }


//}

//async void GetAllBooks()
//{
//    using var context = new ApplicationDbContext();

//    var books = await context.Books.ToListAsync();

//    foreach (var book in books)
//    {
//        Console.WriteLine(book.Title + " _ " + book.ISBN);
//    }
//}

//async void AddBook()
//{
//    Book book = new Book() { Title = "New EF Core Book", ISBN = "12312312", Price = 10.93m, Publisher_Id = 1 };

//    using var context = new ApplicationDbContext();

//    var books = await context.Books.AddAsync(book);
//    await context.SaveChangesAsync();
//}

