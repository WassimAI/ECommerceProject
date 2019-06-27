using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceProject.Models;
using ECommerceProject.Models.MainVM;
using ECommerceProject.Models.ViewModels;

namespace ECommerceProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var collectionsList = new List<CollectionVM>();

            collectionsList = db.collections.ToArray().Select(x => new CollectionVM(x)).ToList();

            HomePageVM model = new HomePageVM
            {
                Collections = collectionsList
            };

            return View(model);
        }
    }
}