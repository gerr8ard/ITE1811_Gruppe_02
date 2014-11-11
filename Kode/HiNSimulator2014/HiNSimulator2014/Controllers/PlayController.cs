using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        private Repository repo = new Repository();
        // GET: Play
        public ActionResult Index()
        {
            return View();
        }

    }
}