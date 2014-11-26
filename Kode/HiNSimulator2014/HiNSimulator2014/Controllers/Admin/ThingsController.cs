using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers.Admin
{
    [Authorize]
    public class ThingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Things
        public ActionResult Index()
        {
            var things = db.Things.Include(t => t.CurrentLocation).Include(t => t.ImageObject);
            return View("~/Views/Admin/Things/Index.cshtml", things.ToList());
        }

        // GET: Things/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thing thing = db.Things.Find(id);
            if (thing == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Things/Details.cshtml", thing);
        }

        // GET: Things/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageText");
            return View("~/Views/Admin/Things/Create.cshtml");
        }

        // POST: Things/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThingID,ImageID,LocationID,Name,Description,IsStationary,KeyLevel,PlayerWritable,WrittenText")] Thing thing)
        {
            if (ModelState.IsValid)
            {
                db.Things.Add(thing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", thing.LocationID);
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageText", thing.ImageID);
            return View("~/Views/Admin/Things/Create.cshtml", thing);
        }

        // GET: Things/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thing thing = db.Things.Find(id);
            if (thing == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", thing.LocationID);
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageText", thing.ImageID);
            return View("~/Views/Admin/Things/Edit.cshtml", thing);
        }

        // POST: Things/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThingID,ImageID,LocationID,Name,Description,IsStationary,KeyLevel,PlayerWritable,WrittenText")] Thing thing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thing).State = EntityState.Modified;
                db.SaveChanges();

                var t = db.Things.Find(thing.ThingID); ;
                db.Entry(t).Reference(x => x.CurrentOwner).Load();
                t.CurrentOwner = null;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(t).Reference(x => x.ArtificialPlayerOwner).Load();
                t.ArtificialPlayerOwner = null;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", thing.LocationID);
            ViewBag.ImageID = new SelectList(db.Images, "ImageID", "ImageText", thing.ImageID);
            return View("~/Views/Admin/Things/Edit.cshtml", thing);
        }

        // GET: Things/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thing thing = db.Things.Find(id);
            if (thing == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Things/Delete.cshtml", thing);
        }

        // POST: Things/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thing thing = db.Things.Find(id);
            db.Things.Remove(thing);
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
