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
    public class ArtificialPlayerResponsesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ArtificialPlayerResponses
        public ActionResult Index()
        {
            var artificialPlayerResponses = db.ArtificialPlayerResponses.Include(a => a.ArtificialPlayer);
            return View("~/Views/Admin/ArtificialPlayerResponses/Index.cshtml", artificialPlayerResponses.ToList());
        }

        // GET: ArtificialPlayerResponses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayerResponse artificialPlayerResponse = db.ArtificialPlayerResponses.Find(id);
            if (artificialPlayerResponse == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ArtificialPlayerResponses/Details.cshtml", artificialPlayerResponse);
        }

        // GET: ArtificialPlayerResponses/Create
        public ActionResult Create()
        {
            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name");
            return View("~/Views/Admin/ArtificialPlayerResponses/Create.cshtml");
        }

        // POST: ArtificialPlayerResponses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtificialPlayerResponseID,ArtificialPlayerID,ResponseText")] ArtificialPlayerResponse artificialPlayerResponse)
        {
            if (ModelState.IsValid)
            {
                db.ArtificialPlayerResponses.Add(artificialPlayerResponse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name", artificialPlayerResponse.ArtificialPlayerID);
            return View("~/Views/Admin/ArtificialPlayerResponses/Create.cshtml", artificialPlayerResponse);
        }

        // GET: ArtificialPlayerResponses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayerResponse artificialPlayerResponse = db.ArtificialPlayerResponses.Find(id);
            if (artificialPlayerResponse == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name", artificialPlayerResponse.ArtificialPlayerID);
            return View("~/Views/Admin/ArtificialPlayerResponses/Edit.cshtml", artificialPlayerResponse);
        }

        // POST: ArtificialPlayerResponses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtificialPlayerResponseID,ArtificialPlayerID,ResponseText")] ArtificialPlayerResponse artificialPlayerResponse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artificialPlayerResponse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name", artificialPlayerResponse.ArtificialPlayerID);
            return View("~/Views/Admin/ArtificialPlayerResponses/Edit.cshtml", artificialPlayerResponse);
        }

        // GET: ArtificialPlayerResponses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayerResponse artificialPlayerResponse = db.ArtificialPlayerResponses.Find(id);
            if (artificialPlayerResponse == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ArtificialPlayerResponses/Delete.cshtml", artificialPlayerResponse);
        }

        // POST: ArtificialPlayerResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtificialPlayerResponse artificialPlayerResponse = db.ArtificialPlayerResponses.Find(id);
            db.ArtificialPlayerResponses.Remove(artificialPlayerResponse);
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
