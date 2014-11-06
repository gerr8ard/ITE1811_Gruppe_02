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
    public class ValidCommandsForArtificialPlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ValidCommandsForArtificialPlayers
        public ActionResult Index()
        {
            var validCommandsForArtificialPlayers = db.ValidCommandsForArtificialPlayers.Include(v => v.ArtificialPlayer).Include(v => v.Command);
            return View("~/Views/Admin/ValidCommandsForArtificialPlayers/Index.cshtml", validCommandsForArtificialPlayers.ToList());
        }

        // GET: ValidCommandsForArtificialPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidCommandsForArtificialPlayers validCommandsForArtificialPlayers = db.ValidCommandsForArtificialPlayers.Find(id);
            if (validCommandsForArtificialPlayers == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ValidCommandsForArtificialPlayers/Details.cshtml", validCommandsForArtificialPlayers);
        }

        // GET: ValidCommandsForArtificialPlayers/Create
        public ActionResult Create()
        {
            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name");
            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name");
            return View("~/Views/Admin/ValidCommandsForArtificialPlayers/Create.cshtml");
        }

        // POST: ValidCommandsForArtificialPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ValidCommandsForArtificialPlayersID,ArtificialPlayerID,CommandID")] ValidCommandsForArtificialPlayers validCommandsForArtificialPlayers)
        {
            if (ModelState.IsValid)
            {
                db.ValidCommandsForArtificialPlayers.Add(validCommandsForArtificialPlayers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name", validCommandsForArtificialPlayers.ArtificialPlayerID);
            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name", validCommandsForArtificialPlayers.CommandID);
            return View("~/Views/Admin/ValidCommandsForArtificialPlayers/Create.cshtml", validCommandsForArtificialPlayers);
        }

        // GET: ValidCommandsForArtificialPlayers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidCommandsForArtificialPlayers validCommandsForArtificialPlayers = db.ValidCommandsForArtificialPlayers.Find(id);
            if (validCommandsForArtificialPlayers == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name", validCommandsForArtificialPlayers.ArtificialPlayerID);
            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name", validCommandsForArtificialPlayers.CommandID);
            return View("~/Views/Admin/ValidCommandsForArtificialPlayers/Edit.cshtml", validCommandsForArtificialPlayers);
        }

        // POST: ValidCommandsForArtificialPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ValidCommandsForArtificialPlayersID,ArtificialPlayerID,CommandID")] ValidCommandsForArtificialPlayers validCommandsForArtificialPlayers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(validCommandsForArtificialPlayers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtificialPlayerID = new SelectList(db.ArtificialPlayers, "ArtificialPlayerID", "Name", validCommandsForArtificialPlayers.ArtificialPlayerID);
            ViewBag.CommandID = new SelectList(db.Commands, "CommandID", "Name", validCommandsForArtificialPlayers.CommandID);
            return View("~/Views/Admin/ValidCommandsForArtificialPlayers/Edit.cshtml", validCommandsForArtificialPlayers);
        }

        // GET: ValidCommandsForArtificialPlayers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValidCommandsForArtificialPlayers validCommandsForArtificialPlayers = db.ValidCommandsForArtificialPlayers.Find(id);
            if (validCommandsForArtificialPlayers == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ValidCommandsForArtificialPlayers/Delete.cshtml", validCommandsForArtificialPlayers);
        }

        // POST: ValidCommandsForArtificialPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValidCommandsForArtificialPlayers validCommandsForArtificialPlayers = db.ValidCommandsForArtificialPlayers.Find(id);
            db.ValidCommandsForArtificialPlayers.Remove(validCommandsForArtificialPlayers);
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
