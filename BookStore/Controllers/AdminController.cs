using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        BookContext db = new BookContext();

        // Add book to the DB
        [HttpGet]
        public ActionResult Add()
        {
            if ((string)Session["Email"] == "admin@bookstore.com")
            {
                ViewBag.BookId = db.Books.Max(item => item.Id) + 1;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpPost]
        public ActionResult Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // Edit book in the DB
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if ((string)Session["Email"] == "admin@bookstore.com")
            {
                var b = db.Books.Find(id);
                return View(b);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpPost]
        public ActionResult Edit(Book b)
        {
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // Delete book from the DB
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if ((string)Session["Email"] == "admin@bookstore.com")
            {
                var b = db.Books.Find(id);
                return View(b);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpPost]
        public ActionResult Delete(Book b)
        {
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // Show all purchases that were made
        public ActionResult PurchasesData()
        {
            if ((string)Session["Email"] == "admin@bookstore.com")
            {
                IEnumerable<Purchase> purchases = db.Purchases;
                ViewBag.Purchases = purchases;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // Show all users that are registered
        public ActionResult UsersData()
        {
            if ((string)Session["Email"] == "admin@bookstore.com")
            {
                IEnumerable<UserProfile> users = db.UserProfiles;
                ViewBag.Users = users;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}