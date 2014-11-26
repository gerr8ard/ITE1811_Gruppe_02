using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private IRepository repo;

        public AdminController()
        {
            repo = new Repository();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateScoreBoard()
        {
            IQueryable<ApplicationUser> users = UserManager.Users.OrderByDescending(x => x.Score);

            Thing scoreboard = repo.GetThingById(56);
            String scoretext = "<br />";
            int index = 1;

            foreach (ApplicationUser user in users)
            {
                scoretext += index++ + ". " + user.Score + "    " + user.PlayerName + "<br />";
                if (index > 5) break;
            }
            scoreboard.WrittenText = scoretext;
            repo.UpdateThing(scoreboard);

            return View();
        }
    }
}