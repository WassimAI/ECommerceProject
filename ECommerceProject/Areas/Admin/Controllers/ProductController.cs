using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ECommerceProject.Extensions;
using ECommerceProject.Models;
using ECommerceProject.Models.Entities;
using ECommerceProject.Models.ViewModels;

namespace ECommerceProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var listOfProducts = new List<ProductVM>();
            listOfProducts = db.products.ToArray().Select(x => new ProductVM(x)).ToList();

            foreach(var product in listOfProducts)
            {
                product.CollectionName = db.collections.Where(x => x.Id.Equals(product.CollectionId)).FirstOrDefault().Title;
            }

            return View(listOfProducts);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ProductVM model = new ProductVM(product);
            model.CollectionName = db.collections.Where(x => x.Id == product.CollectionId).FirstOrDefault().Title;

            return View(model);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title");

            ProductVM model = new ProductVM();

            return View(model);
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM model, HttpPostedFileBase file)
        {
            if (db.products.Any(x => x.Title.Equals(model.Title)))
            {
                ModelState.AddModelError("", "Sorry this product name already exists");
                ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if (file == null || file.ContentLength == 0)
                {
                    ModelState.AddModelError("", "Sorry you did not upload any image");
                    ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title");
                    return View(model);
                }

                Product product = new Product();
                product.Title = model.Title;
                product.Description = model.Description;
                product.oldPrice = model.oldPrice;
                product.stockPrice = model.stockPrice;
                product.isNew = model.isNew;
                product.isOnSale = model.isOnSale;
                product.isFeatured = model.isFeatured;
                product.Discontinued = model.Discontinued;
                product.Discount = model.Discount;
                product.CollectionId = model.CollectionId;
                product.numberInStock = model.numberInStock;

                db.products.Add(product);
                db.SaveChanges();

                //Adding the image
                if(!ExtensionMethods.saveImage(product.Id, file, "Products"))
                {
                    ModelState.AddModelError("", "The image file you uploaded might be of wrong format");
                    ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title");
                    return View(model);
                }
                else
                {
                    product.ImageUrl = file.FileName;
                    db.SaveChanges();
                }

                TempData["success"] = "Product Created successfully";
                return RedirectToAction("Index");
            }

            ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title", model.CollectionId);

            return View(model);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title", product.CollectionId);

            ProductVM model = new ProductVM(product);

            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + id + "/Gallery/Thumbs"))
                                               .Select(fn => Path.GetFileName(fn));

            return View(model);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductVM model, HttpPostedFileBase file)
        {
            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Products/" + model.Id + "/Gallery/Thumbs"))
                                                .Select(fn => Path.GetFileName(fn));

            if (ModelState.IsValid)
            {
                Product product = db.products.Find(model.Id);

                if (file != null && file.ContentLength > 0)
                {
                    ExtensionMethods.DeleteImage(product.Id, "Products");

                    if (ExtensionMethods.saveImage(product.Id, file, "Products"))
                    {
                        product.ImageUrl = file.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("", "The image file you uploaded might be of wrong format");
                        ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title", model.CollectionId);
                        return View(model);
                    }
                }

                product.Title = model.Title;
                product.Description = model.Description;
                product.oldPrice = model.oldPrice;
                product.stockPrice = model.stockPrice;
                product.isNew = model.isNew;
                product.isOnSale = model.isOnSale;
                product.isFeatured = model.isFeatured;
                product.Discontinued = model.Discontinued;
                product.Discount = model.Discount;
                product.CollectionId = model.CollectionId;
                product.numberInStock = model.numberInStock;

                db.SaveChanges();
                TempData["success"] = "Product Updated successfully";
                return RedirectToAction("Index");
            }

            ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title", model.CollectionId);
            return View(model);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ProductVM model = new ProductVM(product);

            return View(model);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.products.Find(id);
            ExtensionMethods.DeleteImage(id, "Products");

            db.products.Remove(product);
            db.SaveChanges();

            TempData["success"] = "Product has been deleted successfully";

            return RedirectToAction("Index");
        }

        // POST: Admin/Product/SaveGalleryImages
        [HttpPost]
        public void SaveGalleryImages(int id)
        {
            // Loop through files
            foreach (string fileName in Request.Files)
            {
                // Init the file
                HttpPostedFileBase file = Request.Files[fileName];

                // Check it's not null
                if (file != null && file.ContentLength > 0)
                {
                    // Set directory paths
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                    string pathString1 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery");
                    string pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery\\Thumbs");

                    //if (!Directory.Exists(pathString1))
                    //    Directory.CreateDirectory(pathString1);

                    //if (!Directory.Exists(pathString2))
                    //    Directory.CreateDirectory(pathString2);

                    // Set image paths
                    var path = string.Format("{0}\\{1}", pathString1, file.FileName);
                    var path2 = string.Format("{0}\\{1}", pathString2, file.FileName);

                    // Save original and thumb

                    file.SaveAs(path);
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(150, 150);
                    img.Save(path2);
                }

            }

        }

        // POST: Admin/Product/DeleteImage
        [HttpPost]
        public void DeleteImage(int id, string imageName)
        {
            string fullPath1 = Request.MapPath("~/Images/Uploads/Products/" + id.ToString() + "/Gallery/" + imageName);
            string fullPath2 = Request.MapPath("~/Images/Uploads/Products/" + id.ToString() + "/Gallery/Thumbs/" + imageName);
            string fullPath3 = Request.MapPath("~/Images/Uploads/Products/" + id.ToString() + "/" + imageName);
            string fullPath4 = Request.MapPath("~/Images/Uploads/Products/" + id.ToString() + "/Thumbs/" + imageName);

            if (System.IO.File.Exists(fullPath1))
                System.IO.File.Delete(fullPath1);

            if (System.IO.File.Exists(fullPath2))
                System.IO.File.Delete(fullPath2);

            if (System.IO.File.Exists(fullPath3))
                System.IO.File.Delete(fullPath3);

            if (System.IO.File.Exists(fullPath4))
                System.IO.File.Delete(fullPath4);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
