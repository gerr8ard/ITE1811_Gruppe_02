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
        private IRepository repository;

        public ThingsController()
        {
            this.repository = new Repository();
        }

        // GET: Things
        public ActionResult Index()
        {
            var things = repository.GetAllThingsWithImage();
            return View("~/Views/Admin/Things/Index.cshtml", things);
        }

        // GET: Things/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thing thing = repository.GetThingById(id);
            if (thing == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Things/Details.cshtml", thing);
        }

        // GET: Things/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName");
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText");
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
                repository.SaveThing(thing);
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", thing.LocationID);
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", thing.ImageID);
            return View("~/Views/Admin/Things/Create.cshtml", thing);
        }

        // GET: Things/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thing thing = repository.GetThingById(id);
            if (thing == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", thing.LocationID);
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", thing.ImageID);
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
                repository.UpdateThing(thing);

                var t = repository.GetThingById(thing.ThingID);
                repository.LoadThing(t);
                t.CurrentOwner = null;
                repository.UpdateThing(t);

                repository.LoadThing(t);
                t.ArtificialPlayerOwner = null;
                repository.UpdateThing(t);

                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", thing.LocationID);
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", thing.ImageID);
            return View("~/Views/Admin/Things/Edit.cshtml", thing);
        }

        // GET: Things/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thing thing = repository.GetThingById(id);
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
            Thing thing = repository.GetThingById(id);
            repository.RemoveThing(thing);
            return RedirectToAction("Index");
        }
    }
}
