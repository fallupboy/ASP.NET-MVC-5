﻿using System;
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

        // Show all books on the main page
        public ActionResult Index()
        {
            var bookList = db.Books.OrderBy(book => book.Name).ToList();
            ViewBag.Books = bookList;
            return View();
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
                return RedirectToAction("Login", "Users");
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
    }
}