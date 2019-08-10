using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ECommerceProject.Models;
using ECommerceProject.Models.Entities;
using ECommerceProject.Models.ViewModels;

namespace ECommerceProject.Controllers
{
    public class UserAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult Register()
        {
            RegisterUserVM model = new RegisterUserVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterUserVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill in the requirements below");
                return View(model);
            }

            //check email
            if (db.usersAccounts.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("", "Sorry, this Email already exists");
                return View(model);
            }

            UserAccount user = new UserAccount()
            {
                UserID = Guid.NewGuid(),
                Password = Crypto.Hash(model.Password, "sha256"),
                Email = model.Email,
                Country = model.Country,
                City = model.City,
                Province = model.Province,
                Street = model.Street,
                Residence = model.Residence,
                Phone = model.Phone
            };

            db.usersAccounts.Add(user);
            db.SaveChanges();

            TempData["success"] = "You have successfully registered, you can now log in with your username / email and password";

            return RedirectToAction("Login", "UserAccount");
        }

        public ActionResult Login()
        {
            LoginVM model = new LoginVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please make sure you enter correct data");
                return View(model);
            }

            string hashedPassword = Crypto.Hash(model.Password, "sha256");

            UserAccount user = db.usersAccounts.Where(x => x.Email == model.Email && x.Password==hashedPassword).FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("", "Wrong Username or Password");
                return View(model);
            }

            Session["UserId"] = user.UserID;
            Session["Email"] = user.Email;

            return RedirectToAction("all-products", "Product");
        }

        [HttpPost]
        public JsonResult Logout(string returnUrl)
        {
            Session.Clear();
            var result = new { returnUrl = returnUrl};

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}