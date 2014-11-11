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

        /*
        public ActionResult Movement(int? id)
        {
            // Mottar den nye posisjonen til spilleren, og oppdaterer feltet i databasen
            if (id != null)
            {
                repo.UpdatePlayerLocation(User.Identity.Name, (int)id);
                return View(repo.GetConnectedLocations((int)id));
            }
            else
            {
                // Henter lagret posisjon fra databasen
                var user = repo.GetUser(User.Identity.Name);
                if (user.CurrentLocation != null)
                {
                    return View(repo.GetConnectedLocations(user.CurrentLocation.LocationID));
                }
                else
                { // Default location er Glassgata
                    return View(repo.GetConnectedLocations(repo.GetLocation("Glassgata").LocationID));
                }
            }
        
        }
         * */
    }
}