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
    public class LocationConnectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LocationConnections
        public ActionResult Index()
        {
            var locationConnections = db.LocationConnections.Include(l => l.LocationOne).Include(l => l.LocationTwo);
            return View("~/Views/Admin/LocationConnections/Index.cshtml", locationConnections.ToList());
        }

        // GET: LocationConnections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationConnection locationConnection = db.LocationConnections.Find(id);
            if (locationConnection == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/LocationConnections/Details.cshtml", locationConnection);
        }

        // GET: LocationConnections/Create
        public ActionResult Create()
        {
            ViewBag.LocationOne_LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            ViewBag.LocationTwo_LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            return View("~/Views/Admin/LocationConnections/Create.cshtml");
        }

        // POST: LocationConnections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationConnectionID,IsLocked,RequiredKeyLevel,LocationOne_LocationID,LocationTwo_LocationID")] LocationConnection locationConnection)
        {
            if (ModelState.IsValid)
            {
                db.LocationConnections.Add(locationConnection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationOne_LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationConnection.LocationOne_LocationID);
            ViewBag.LocationTwo_LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationConnection.LocationTwo_LocationID);
            return View("~/Views/Admin/LocationConnections/Create.cshtml", locationConnection);
        }

        // GET: LocationConnections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationConnection locationConnection = db.LocationConnections.Find(id);
            if (locationConnection == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationOne_LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationConnection.LocationOne_LocationID);
            ViewBag.LocationTwo_LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationConnection.LocationTwo_LocationID);
            return View("~/Views/Admin/LocationConnections/Edit.cshtml", locationConnection);
        }

        // POST: LocationConnections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationConnectionID,IsLocked,RequiredKeyLevel,LocationOne_LocationID,LocationTwo_LocationID")] LocationConnection locationConnection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationConnection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationOne_LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationConnection.LocationOne_LocationID);
            ViewBag.LocationTwo_LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationConnection.LocationTwo_LocationID);
            return View("~/Views/Admin/LocationConnections/Edit.cshtml", locationConnection);
        }

        // GET: LocationConnections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationConnection locationConnection = db.LocationConnections.Find(id);
            if (locationConnection == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/LocationConnections/Delete.cshtml", locationConnection);
        }

        // POST: LocationConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationConnection locationConnection = db.LocationConnections.Find(id);
            db.LocationConnections.Remove(locationConnection);
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
