using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceProject.Models;
using ECommerceProject.Models.Entities;
using ECommerceProject.Models.MainVM;
using ECommerceProject.Models.ViewModels;

namespace ECommerceProject.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        [ActionName("all-products")]
        public ActionResult AllProducts()
        {
            var productsList = new List<ProductVM>();

            productsList = db.products.ToArray().Select(x => new ProductVM(x)).ToList();

            ProductsPageVM model = new ProductsPageVM
            {
                Products = productsList
            };
            
            return View(model);
        }

        public ActionResult ProductDetailsPartial(int id) 
        {
            Product product = db.products.Find(id);
            ProductVM model = new ProductVM(product);

            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + id + "/Gallery/Thumbs"))
                                               .Select(fn => Path.GetFileName(fn));

            return View("_productDetailsPartial", model);
        }

        [ActionName("featured-products")]
        public ActionResult FeaturedProducts()
        {
            var list = new List<ProductVM>();
            list = db.products.ToArray().Where(x=> x.isFeatured == true).Select(x => new ProductVM(x)).ToList();

            return View(list);
        }

        [ActionName("promotions")]
        public ActionResult PromotedProducts()
        {
            var list = new List<ProductVM>();
            list = db.products.ToArray().Where(x => x.isOnSale == true).Select(x => new ProductVM(x)).ToList();

            return View(list);
        }
    }
}