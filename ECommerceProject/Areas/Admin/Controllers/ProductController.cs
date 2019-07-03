using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
            model.CollectionName = db.collections.Where(x => x.Id.Equals(product.Id)).FirstOrDefault().Title;

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
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ImageUrl,oldPrice,newPrice,stockPrice,Discount,isNew,numberInStock,isOnSale,Discontinued,CollectionId,isFeatured")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollectionId = new SelectList(db.collections, "Id", "Title", product.CollectionId);
            return View(product);
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
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
