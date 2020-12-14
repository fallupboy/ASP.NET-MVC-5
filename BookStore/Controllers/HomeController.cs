using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BookStore.Models;
using Microsoft.Ajax.Utilities;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        // Encrypt user password
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        // Register page
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserProfile _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.UserProfiles.FirstOrDefault(s => s.UserEmail == _user.UserEmail);
                if (check == null)
                {
                    _user.UserPassword = GetMD5(_user.UserPassword);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.UserProfiles.Add(_user);
                    db.SaveChanges();
                    Session.Clear(); // remove session
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists. Choose another one.";
                    return View();
                }
            }
            return View();
        }

        // Login page
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.UserProfiles.FirstOrDefault(s => s.UserEmail == email && s.UserPassword == f_password);
                if (data != null)
                {
                    // add session
                    Session["FullName"] = data.UserFirstName + " " + data.UserSecondName;
                    Session["Email"] = data.UserEmail;
                    Session["idUser"] = data.UserId;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Incorrect login or password! Try again.";
                    return View();
                }
            }
            return View();
        }

        // Show active user profile
        public ActionResult UserDashBoard()
        {
            IEnumerable<Purchase> purchases = db.Purchases;
            ViewBag.Purchases = purchases;
            if (Session["Email"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear(); // remove session
            return RedirectToAction("Login");
        }

        // Show all books on the main page
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
            if (Session["idUser"] != null)
            {
                ViewBag.BookId = id;
                ViewBag.UserId = Session["idUser"];
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult Buy(Purchase purchase)
        {
            IEnumerable<Book> books = db.Books;
            foreach (var b in books)
            {
                if (b.Id == purchase.BookId)
                {
                    if (b.Quantity > 0)
                    {
                        b.Quantity--;
                        break;
                    }
                    else
                    {
                        return RedirectToAction("BuyFailed");
                    }
                }
            }
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

        // Failed purchase
        public ActionResult BuyFailed()
        {
            return View();
        }

        // Show all purchases that were made
        public ActionResult Purchases()
        {
            IEnumerable<Purchase> purchases = db.Purchases;
            ViewBag.Purchases = purchases;
            return View();
        }

        // Show all users that were made
        public ActionResult Users()
        {
            IEnumerable<UserProfile> users = db.UserProfiles;
            ViewBag.Users = users;
            return View();
        }
    }
}