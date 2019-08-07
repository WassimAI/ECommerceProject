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
    public class CollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Collection
        [ActionName("all-collections")]
        public ActionResult AllCollections()
        {
            var listOfCollections = db.collections.ToArray().Select(x => new CollectionVM(x)).ToList();

            CollectionsPageVM model = new CollectionsPageVM
            {
                Collections = listOfCollections
            };

            return View(model);
        }
    }
}