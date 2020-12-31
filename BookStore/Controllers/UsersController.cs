using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class UsersController : Controller
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
                var emailCheck = db.UserProfiles.FirstOrDefault(s => s.Email == _user.Email);
                if (emailCheck == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.UserProfiles.Add(_user);
                    db.SaveChanges();
                    Session.Clear(); // remove session
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.RegistrationEmailError = "Email already exists. Choose another one.";
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
                var data = db.UserProfiles.FirstOrDefault(s => s.Email == email && s.Password == f_password);
                if (data != null)
                {
                    // add session
                    Session["FullName"] = data.FirstName + " " + data.LastName;
                    Session["Email"] = data.Email;
                    Session["idUser"] = data.Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginError = "Incorrect login or password! Try again.";
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
    }
}