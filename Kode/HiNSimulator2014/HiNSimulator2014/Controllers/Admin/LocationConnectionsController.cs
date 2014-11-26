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
        private IRepository repository;

        public LocationConnectionsController()
        {
            this.repository = new Repository();
        }

        // GET: LocationConnections
        public ActionResult Index()
        {
            var locationConnections = repository.GetAllConnectedLocations();
            return View("~/Views/Admin/LocationConnections/Index.cshtml", locationConnections);
        }

        // GET: LocationConnections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationConnection locationConnection = repository.GetLocationConnected(id);
            if (locationConnection == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/LocationConnections/Details.cshtml", locationConnection);
        }

        // GET: LocationConnections/Create
        public ActionResult Create()
        {
            ViewBag.LocationOne_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName");
            ViewBag.LocationTwo_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName");
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
                repository.SaveLocationConnected(locationConnection);
                return RedirectToAction("Index");
            }

            ViewBag.LocationOne_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", locationConnection.LocationOne_LocationID);
            ViewBag.LocationTwo_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", locationConnection.LocationTwo_LocationID);
            return View("~/Views/Admin/LocationConnections/Create.cshtml", locationConnection);
        }

        // GET: LocationConnections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationConnection locationConnection = repository.GetLocationConnected(id);
            if (locationConnection == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationOne_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", locationConnection.LocationOne_LocationID);
            ViewBag.LocationTwo_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", locationConnection.LocationTwo_LocationID);
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
                repository.UpdateLocationConnected(locationConnection);
                return RedirectToAction("Index");
            }
            ViewBag.LocationOne_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", locationConnection.LocationOne_LocationID);
            ViewBag.LocationTwo_LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", locationConnection.LocationTwo_LocationID);
            return View("~/Views/Admin/LocationConnections/Edit.cshtml", locationConnection);
        }

        // GET: LocationConnections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationConnection locationConnection = repository.GetLocationConnected(id);
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
            LocationConnection locationConnection = repository.GetLocationConnected(id);
            repository.RemoveLocationConnected(locationConnection);
            return RedirectToAction("Index");
        }

    }
}
