using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers
{
    public class HomeController : Controller
    {
        private Repository repo = new Repository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowUser()
        {
            ViewBag.Message = "Your contact page.";
            
            return View(repo.GetUser(User.Identity.Name));
        }

        [Authorize]
        public ActionResult ShowLocations(int? id)
        {
            // Mottar den nye posisjonen til spilleren, og oppdaterer feltet i databasen
            if (id != null) {
                repo.UpdatePlayerLocation(User.Identity.Name, (int)id);
                return View(repo.GetConnectedLocations((int)id));
            }
            else
            {
                // Henter lagret posisjon fra databasen
                var user = repo.GetUser(User.Identity.Name);
                return View(repo.GetConnectedLocations(user.CurrentLocation.LocationID));
            }
                
        }
    }
}