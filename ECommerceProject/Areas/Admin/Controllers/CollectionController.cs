using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceProject.Models;
using ECommerceProject.Models.Entities;
using ECommerceProject.Models.ViewModels;
using ECommerceProject.Extensions;

namespace ECommerceProject.Areas.Admin.Controllers
{
    public class CollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Collection
        public ActionResult Index()
        {
            var collections = db.collections.ToArray().Select(x => new CollectionVM(x)).ToList();

            foreach (var collection in collections)
            {
                collection.VendorName = db.vendors.Where(x => x.Id.Equals(collection.VendorId)).FirstOrDefault().Title;
            }

            return View(collections);
        }

        // GET: Admin/Collection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.collections.Find(id);

            CollectionVM model = new CollectionVM(collection);
            model.VendorName = db.vendors.Where(x => x.Id.Equals(model.VendorId)).FirstOrDefault().Title;

            if (collection == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // GET: Admin/Collection/Create
        public ActionResult Create()
        {
            ViewBag.VendorId = new SelectList(db.vendors, "Id", "Title");

            CollectionVM model = new CollectionVM();

            return View(model);
        }

        // POST: Admin/Collection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CollectionVM model, HttpPostedFileBase file)
        {
            if (db.collections.Any(x => x.Title.Equals(model.Title)))
            {
                ModelState.AddModelError("", "Sorry, the collection name already exists");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                Collection collection = new Collection
                {
                    Title = model.Title,
                    Description = model.Description,
                    VendorId = model.VendorId
                };

                db.collections.Add(collection);
                db.SaveChanges();

                if(!ExtensionMethods.saveImage(collection.Id, file))
                {
                    ModelState.AddModelError("", "The image file you uploaded might be of wrong format");
                    return View(model);
                }
                else
                {
                    collection.ImageUrl = file.FileName;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.VendorId = new SelectList(db.vendors, "Id", "Title", model.VendorId);

            return View(model);
        }

        // GET: Admin/Collection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Collection collection = db.collections.Find(id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            CollectionVM model = new CollectionVM(collection);
            model.VendorName = db.vendors.Where(x => x.Id.Equals(model.VendorId)).FirstOrDefault().Title;

            ViewBag.VendorId = new SelectList(db.vendors, "Id", "Title", model.VendorId);
            return View(model);
        }

        // POST: Admin/Collection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CollectionVM model)
        {
            if (db.collections.Where(x => x.Title != model.Title).Any(x => x.Title.Equals(model.Title)))
            {
                ModelState.AddModelError("", "Sorry this collection name already exists.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                Collection collection = db.collections.Where(x => x.Id.Equals(model.Id)).FirstOrDefault();

                collection.Title = model.Title;
                collection.Description = model.Description;
                collection.ImageUrl = model.ImageUrl;
                collection.VendorId = model.VendorId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VendorId = new SelectList(db.vendors, "Id", "Title", model.VendorId);
            return View(model);
        }

        // GET: Admin/Collection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }

            CollectionVM model = new CollectionVM(collection);
            model.VendorName = db.vendors.Where(x => x.Id.Equals(model.VendorId)).FirstOrDefault().Title;

            return View(model);
        }

        // POST: Admin/Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collection collection = db.collections.Find(id);

            db.collections.Remove(collection);
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
