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
    public class ValidCommandsForThingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ValidCommandsForThings
        public ActionResult Index()
        {
            var validCommandsForThings = db.ValidCommandsForThings.Include(v => v.Command).Include(v => v.Thing);
            return View("~/Views/Admin/ValidCommandsForThings/Index.cshtml", validCommandsForThings.ToList());
        }

        // GET: ValidCommandsForThings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidCommandsForThings validCommandsForThings = db.ValidCommandsForThings.Find(id);
            if (validCommandsForThings == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ValidCommandsForThings/Details.cshtml", validCommandsForThings);
        }

        // GET: ValidCommandsForThings/Create
        public ActionResult Create()
        {
            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name");
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name");
            return View("~/Views/Admin/ValidCommandsForThings/Create.cshtml");
        }

        // POST: ValidCommandsForThings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ValidCommandsForThingsID,ThingID,CommandID")] ValidCommandsForThings validCommandsForThings)
        {
            if (ModelState.IsValid)
            {
                db.ValidCommandsForThings.Add(validCommandsForThings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name", validCommandsForThings.CommandID);
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", validCommandsForThings.ThingID);
            return View("~/Views/Admin/ValidCommandsForThings/Create.cshtml", validCommandsForThings);
        }

        // GET: ValidCommandsForThings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidCommandsForThings validCommandsForThings = db.ValidCommandsForThings.Find(id);
            if (validCommandsForThings == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name", validCommandsForThings.CommandID);
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", validCommandsForThings.ThingID);
            return View("~/Views/Admin/ValidCommandsForThings/Edit.cshtml", validCommandsForThings);
        }

        // POST: ValidCommandsForThings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ValidCommandsForThingsID,ThingID,CommandID")] ValidCommandsForThings validCommandsForThings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(validCommandsForThings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name", validCommandsForThings.CommandID);
            ViewBag.ThingID = new SelectList(db.Things, "ThingID", "Name", validCommandsForThings.ThingID);
            return View("~/Views/Admin/ValidCommandsForThings/Edit.cshtml", validCommandsForThings);
        }

        // GET: ValidCommandsForThings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidCommandsForThings validCommandsForThings = db.ValidCommandsForThings.Find(id);
            if (validCommandsForThings == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ValidCommandsForThings/Delete.cshtml", validCommandsForThings);
        }

        // POST: ValidCommandsForThings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValidCommandsForThings validCommandsForThings = db.ValidCommandsForThings.Find(id);
            db.ValidCommandsForThings.Remove(validCommandsForThings);
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
