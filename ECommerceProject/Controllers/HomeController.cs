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
            var featuredList = new List<ProductVM>();
            var promotedList = new List<ProductVM>();

            collectionsList = db.collections.ToArray().Select(x => new CollectionVM(x)).ToList();
            featuredList = db.products.ToArray().Where(x => x.isFeatured == true).OrderByDescending(x => x.Id).Take(2).Select(x => new ProductVM(x)).ToList();
            promotedList = db.products.ToArray().Where(x => x.isOnSale == true).OrderByDescending(x => x.Id).Take(2).Select(x => new ProductVM(x)).ToList();

            HomePageVM model = new HomePageVM
            {
                Collections = collectionsList,
                FeaturedProducts = featuredList,
                PromotedProducts = promotedList
            };

            return View(model);
        }
    }
}