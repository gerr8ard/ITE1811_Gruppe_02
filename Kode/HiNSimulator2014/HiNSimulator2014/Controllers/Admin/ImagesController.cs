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
    [Authorize]
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Repository repo = new Repository();

        // GET: Images
        public ActionResult Index()
        {
            return View("~/Views/Admin/Images/Index.cshtml", repo.GetAllImages());
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageID,ImageText,ImageBlob")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Admin/Images/Create.cshtml", image);
        }

        // Laster opp bilde
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            // http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.fileupload.filebytes(v=vs.110).aspx

            if (file != null)
            {
                if (file.ContentType.Contains("image"))
                {
                    Debug.Write(file.ContentType);
                    // http://scottlilly.com/how-to-upload-a-file-in-an-asp-net-mvc-4-page/
                    byte[] imageBytes = new byte[file.ContentLength];
                    // Skal være mindre enn 1MB
                    if (imageBytes.Length < 1000000)
                    {
                        file.InputStream.Read(imageBytes, 0, Convert.ToInt32(file.ContentLength));

                        String imageText = Request.Form["imageText"];

                        Image image = new Image
                        {
                            ImageText = imageText,
                            ImageBlob = imageBytes
                        };

                        repo.SaveImageToDB(image);
                    }
                    else
                    {
                        ViewBag.Message = "Image is too large";
                        return RedirectToAction("Create");
                    }
                }
                else
                {
                    ViewBag.Message = "File is not an image";
                    return RedirectToAction("Create");
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
            Image image = db.Images.Find(id);
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
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
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
            Image image = db.Images.Find(id);
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
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
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
