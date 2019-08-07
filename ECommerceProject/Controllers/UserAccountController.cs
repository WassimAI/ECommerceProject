using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            UserAccount user = new UserAccount()
            {
                UserID = Guid.NewGuid(),
                UserName = model.UserName,
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

            return RedirectToAction("Index", "Home");
        }
    }
}