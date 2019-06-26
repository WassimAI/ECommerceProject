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

namespace ECommerceProject.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Vendor
        public ActionResult Index()
        {
            return View(db.vendors.ToArray().Select(x=> new VendorVM(x)).ToList());
        }

        // GET: Admin/Vendor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendor vendor = db.vendors.Find(id);

            VendorVM model = new VendorVM(vendor);

            if (vendor == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // GET: Admin/Vendor/Create
        public ActionResult Create()
        {
            VendorVM model = new VendorVM();

            return View(model);
        }

        // POST: Admin/Vendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendorVM model)
        {
            if (ModelState.IsValid)
            {
                //Look for existing vendors
                if(db.vendors.Any(x=> x.Title.Equals(model.Title)))
                {
                    ModelState.AddModelError("", "Sorry, this vendor name already exists");
                    return View(model);
                }

                Vendor vendor = new Vendor();

                vendor.Title = model.Title;
                vendor.Location = model.Location;
                vendor.IsActive = model.IsActive;

                db.vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Vendor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendor vendor = db.vendors.Find(id);

            if (vendor == null)
            {
                return HttpNotFound();
            }

            VendorVM model = new VendorVM(vendor);

            
            return View(model);
        }

        // POST: Admin/Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VendorVM model)
        {
            if (db.vendors.Where(x=>x.Id != model.Id).Any(x => x.Title == model.Title))
            {
                ModelState.AddModelError("", "This Name Already exist");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                Vendor vendor = db.vendors.Where(x => x.Id.Equals(model.Id)).FirstOrDefault();

                vendor.Title = model.Title;
                vendor.Location = model.Location;
                vendor.IsActive = model.IsActive;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Vendor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendor vendor = db.vendors.Find(id);

            if (vendor == null)
            {
                return HttpNotFound();
            }

            VendorVM model = new VendorVM(vendor);

            return View(model);
        }

        // POST: Admin/Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.vendors.Find(id);

            if (db.collections.Any(x => x.VendorId.Equals(id)))
            {
                ModelState.AddModelError("", "There are collections attached to this vendor, you must delete collections first or assign them to another vendor");
                VendorVM model = new VendorVM
                {
                    Id = vendor.Id,
                    Title = vendor.Title,
                    Location = vendor.Location,
                    IsActive = vendor.IsActive
                };

                return View(model);
            }

            db.vendors.Remove(vendor);
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
