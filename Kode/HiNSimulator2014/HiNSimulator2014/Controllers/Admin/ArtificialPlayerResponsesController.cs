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
        private IRepository repository;

        public ArtificialPlayerResponsesController()
        {
            this.repository = new Repository();
        }

        // GET: ArtificialPlayerResponses
        public ActionResult Index()
        {
            var artificialPlayerResponses = repository.GetAllArtificialPlayerResponses();

            return View("~/Views/Admin/ArtificialPlayerResponses/Index.cshtml", artificialPlayerResponses);
        }

        // GET: ArtificialPlayerResponses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayerResponse artificialPlayerResponse = repository.GetArtificialPlayerResponse(id);
            if (artificialPlayerResponse == null)
            {
                return HttpNotFound();
            }

            return View("~/Views/Admin/ArtificialPlayerResponses/Details.cshtml", artificialPlayerResponse);
        }

        // GET: ArtificialPlayerResponses/Create
        public ActionResult Create()
        {
            ViewBag.ArtificialPlayerID = new SelectList(repository.GetArtificialPlayerSet(), "ArtificialPlayerID", "Name");

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
                repository.SaveArtificialPlayerResponse(artificialPlayerResponse);
                return RedirectToAction("Index");
            }

            ViewBag.ArtificialPlayerID = new SelectList(repository.GetArtificialPlayerSet(), "ArtificialPlayerID", "Name", artificialPlayerResponse.ArtificialPlayerID);

            return View("~/Views/Admin/ArtificialPlayerResponses/Create.cshtml", artificialPlayerResponse);
        }

        // GET: ArtificialPlayerResponses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayerResponse artificialPlayerResponse = repository.GetArtificialPlayerResponse(id);
            if (artificialPlayerResponse == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtificialPlayerID = new SelectList(repository.GetArtificialPlayerSet(), "ArtificialPlayerID", "Name", artificialPlayerResponse.ArtificialPlayerID);

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
                repository.UpdateArtificialPlayerResponse(artificialPlayerResponse);
                return RedirectToAction("Index");
            }
            ViewBag.ArtificialPlayerID = new SelectList(repository.GetArtificialPlayerSet(), "ArtificialPlayerID", "Name", artificialPlayerResponse.ArtificialPlayerID);

            return View("~/Views/Admin/ArtificialPlayerResponses/Edit.cshtml", artificialPlayerResponse);
        }

        // GET: ArtificialPlayerResponses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtificialPlayerResponse artificialPlayerResponse = repository.GetArtificialPlayerResponse(id);
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
            ArtificialPlayerResponse artificialPlayerResponse = repository.GetArtificialPlayerResponse(id);
            repository.RemoveArtificialPlayerResponse(artificialPlayerResponse);

            return RedirectToAction("Index");
        }

    }
}
