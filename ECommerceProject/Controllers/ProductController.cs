using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [ActionName("all-products")]
        public ActionResult AllProducts()
        {
            return View();
        }
    }
}