using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HiNSimulator2014.Models;
using System.IO;
using System.Diagnostics;

namespace HiNSimulator2014.Controllers.Admin
{
    /// <summary>
    /// ImagesController - mottar opplastet bilde og lagrer det i databasen
    /// Sjekker på størrelse og filtype
    /// Modifisert av Andras Dyrøy Jansson
    /// </summary>
    [Authorize]
    public class ImagesController : Controller
    {
        private IRepository repository;

        public ImagesController()
        {
            this.repository = new Repository();
        }

        // GET: Images
        // Viser liste over alle bilder i systemet
        public ActionResult Index()
        {
            return View("~/Views/Admin/Images/Index.cshtml", repository.GetAllImages());
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = repository.GetImage(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Images/Details.cshtml", image);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View("~/Views/Admin/Images/Create.cshtml");
        }

        // POST: Images/Create
        // Mottar opplastet fil fra bruker
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)
        {

            // http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.fileupload.filebytes(v=vs.110).aspx
            // Sjekker om filen eksisterer
            if (file != null)
            {
                // Sjekker på MIMEtype
                if (file.ContentType.Contains("image"))
                {
                    Debug.Write(file.ContentType);
                    // http://scottlilly.com/how-to-upload-a-file-in-an-asp-net-mvc-4-page/
                    byte[] imageBytes = new byte[file.ContentLength];
                    // Skal være mindre enn 80Kb
                    if (imageBytes.Length < 80000)
                    {
                        // Konverterer til byte-array
                        file.InputStream.Read(imageBytes, 0, Convert.ToInt32(file.ContentLength));

                        // Bildetekst
                        String imageText = Request.Form["imageText"];

                        // Oppretter et nytt Image-objekt
                        Image image = new Image
                        {
                            ImageText = imageText,
                            ImageBlob = imageBytes,
                            MimeType = file.ContentType
                        };
                        // Lagrer bildet i databasen
                        repository.SaveImageToDB(image);
                    }
                    else
                    {
                        // Viser feilmelding
                        ViewBag.Message = "Image is too large: " + (file.ContentLength / 1000) + "Kb";
                        return View("~/Views/Admin/Images/Create.cshtml");
                    }
                }
                else
                {
                    // Viser feilmelding
                    ViewBag.Message = "File '" + file.FileName + "' is not an image";
                    return View("~/Views/Admin/Images/Create.cshtml");
                }


            }
            //Display records
            return RedirectToAction("Index");
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = repository.GetImage(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Images/Edit.cshtml", image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,ImageText,ImageBlob")] Image image)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateImage(image);
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/Images/Edit.cshtml", image);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = repository.GetImage(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Images/Delete.cshtml", image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteImage(id);
            return RedirectToAction("Index");
        }

    }
}
