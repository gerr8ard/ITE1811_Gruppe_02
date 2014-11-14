using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiNSimulator2014.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        // GET: Play
        public ActionResult Index()
        {
            return View();
        }

    }
}