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
    public class ArtificialPlayersController : Controller
    {
        private IRepository repository;

        public ArtificialPlayersController()
        {
            this.repository = new Repository();
        }

        // GET: ArtificialPlayers
        public ActionResult Index()
        {
            var artificialPlayers = repository.GetAllArtificialPlayersWithImagesAndLocations();
            return View("~/Views/Admin/ArtificialPlayers/Index.cshtml", artificialPlayers);
        }

        // GET: ArtificialPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayer artificialPlayer = repository.GetArtificialPlayer(id);
            if (artificialPlayer == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ArtificialPlayers/Details.cshtml", artificialPlayer);
        }

        // GET: ArtificialPlayers/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName");
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText");
            return View("~/Views/Admin/ArtificialPlayers/Create.cshtml");
        }

        // POST: ArtificialPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtificialPlayerID,Name,Type,Description,AccessLevel,IsStationary,LocationID,ImageID")] ArtificialPlayer artificialPlayer)
        {
            if (ModelState.IsValid)
            {
                repository.SaveArtificialPlayer(artificialPlayer);
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", artificialPlayer.LocationID);
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", artificialPlayer.ImageID);
            return View("~/Views/Admin/ArtificialPlayers/Create.cshtml", artificialPlayer);
        }

        // GET: ArtificialPlayers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayer artificialPlayer = repository.GetArtificialPlayer(id);
            if (artificialPlayer == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", artificialPlayer.LocationID);
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", artificialPlayer.ImageID);
            return View("~/Views/Admin/ArtificialPlayers/Edit.cshtml", artificialPlayer);
        }

        // POST: ArtificialPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtificialPlayerID,Name,Type,Description,AccessLevel,IsStationary,LocationID,ImageID")] ArtificialPlayer artificialPlayer)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateArtificialPlayer(artificialPlayer);
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(repository.GetLocationSet(), "LocationID", "LocationName", artificialPlayer.LocationID);
            ViewBag.ImageID = new SelectList(repository.GetImageSet(), "ImageID", "ImageText", artificialPlayer.ImageID);
            return View("~/Views/Admin/ArtificialPlayers/Edit.cshtml", artificialPlayer);
        }

        // GET: ArtificialPlayers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayer artificialPlayer = repository.GetArtificialPlayer(id);
            if (artificialPlayer == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/ArtificialPlayers/Delete.cshtml", artificialPlayer);
        }

        // POST: ArtificialPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtificialPlayer artificialPlayer = repository.GetArtificialPlayer(id);
            repository.RemoveArtificialPlayer(artificialPlayer);
            return RedirectToAction("Index");
        }

    }
}
