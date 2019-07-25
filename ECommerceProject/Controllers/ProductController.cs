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
    public class ProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
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
    }
}