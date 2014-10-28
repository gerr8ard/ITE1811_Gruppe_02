using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers
{
    public class TestDataController : Controller
    {
        private Repository repository;

        public TestDataController()
        {
            repository = new Repository();
        }

        //
        // GET: /TestData/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCommands()
        {
            return View(repository.GetAllCommands());
        }

        public ActionResult ShowLocations()
        {
            return View(repository.GetAllLocations());
        }

        //
        // GET: /TestData/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TestData/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TestData/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TestData/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TestData/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TestData/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TestData/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
