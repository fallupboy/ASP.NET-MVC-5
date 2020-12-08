using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        // Show all books on page
        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;
            ViewBag.Books = books;
            return View();
        }

        // Add book to the DB
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        // Edit book in the DB
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var b = db.Books.Find(id);
            return View(b);
        }
        [HttpPost]
        public ActionResult Edit(Book b)
        {   
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        // Buy book
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return RedirectToAction("BuySuccesful");
        }

        // Successful purchase
        public ActionResult BuySuccesful()
        {
            return View();
        }

        // Show all the purchases that were made
        public ActionResult Purchases()
        {
            IEnumerable<Purchase> purchases = db.Purchases;
            ViewBag.Purchases = purchases;
            return View();
        }
    }
}