using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using HiNSimulator2014.Models;
using System.Diagnostics;
using HiNSimulator2014.Classes;


namespace HiNSimulator2014.Controllers.WebApi
{

    /// <summary>
    /// WebAPI kontroller som brukes for å hente ut brukernavn og bruker id.
    /// 
    /// Skrevet av Pål Gerrard Gaare-Skogsrud
    /// </summary>
    public class ChatController : ApiController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Metode som henter ut pålogget brukers brukernavn.
        /// </summary>
        /// <returns>PlayerName</returns>
        public String GetUsername()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return user.PlayerName;
        }

        /// <summary>
        /// Metode som henter pålogget brukers brukerid.
        /// </summary>
        /// <returns>Id</returns>
        public String getUserId()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return user.Id;
        }
        
    }
}
