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
using HiNSimulator2014.Classes;

namespace HiNSimulator2014.Controllers.WebApi
{
    public class ArtificialPlayerController : ApiController
    {
        private const int PUNCH_PROBABILITY = 35; // sannynlighet for vellykket slag
        private const int KICK_PROBABILITY = 20; // sannsynlighet for vellykket spark

        private IRepository repository;
        private ApplicationUserManager _userManager;
        private Random rand;

        public ArtificialPlayerController()
        {
            this.repository = new Repository();
            this.rand = new Random(DateTime.Now.Ticks.GetHashCode());
        }

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

        // GET: api/ArtificialPlayer/GetArtificialPlayersInLocation/5
        [HttpGet]
        public List<SimpleArtificialPlayer> GetArtificialPlayersInLocation(int id)
        {
            List<SimpleArtificialPlayer> simpleList = new List<SimpleArtificialPlayer>();
            foreach (ArtificialPlayer ap in repository.GetArtificialPlayerInLocation(repository.GetLocation(id)))
            {
                simpleList.Add(new SimpleArtificialPlayer { ArtificialPlayerID = ap.ArtificialPlayerID, Name = ap.Name });
            }
            return simpleList;
        }

        [HttpGet]
        public SimpleArtificialPlayer GetArtificialPlayerInfo(int id)
        {
            ArtificialPlayer ap = repository.GetArtificialPlayer(id);
            return new SimpleArtificialPlayer
            {
                ArtificialPlayerID = ap.ArtificialPlayerID,
                Name = ap.Name,
                Type = ap.Type,
                Description = ap.Description,
                ImageID = ap.ImageID
            };
        }
        
        // Returnerer en tilfeldig respons for en kunstig aktør
        [HttpGet]
        public String GetArtificialPlayerResponse(int id)
        {
            List<ArtificialPlayerResponse> allResponses = repository.GetAllResponsesForArtificialPlayer(id);
            if (allResponses.Count > 0)
            {
                Random r = new Random();
                return allResponses.ElementAt(r.Next(0, allResponses.Count)).ResponseText;
            }
            return null;
        }

        // Returnerer en string som forteller hva som skjedde
        [HttpGet]
        public String PunchArtificialPlayer(int id)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            ArtificialPlayer ap = repository.GetArtificialPlayer(id);
            
            if (rand.Next(0, 101) > PUNCH_PROBABILITY)
            {
                int scoreloss = 100 - rand.Next(0, 100 - PUNCH_PROBABILITY);
                user.Score -= scoreloss;
                UserManager.Update(user);
                return "You underestimated " + ap.Name + ". He dodges and you hit yourself in the face. " + scoreloss + " was deducted from your score.";
            }
            else {
                int scoregain = 100 - rand.Next(0, 100 - PUNCH_PROBABILITY);
                user.Score += scoregain;
                UserManager.Update(user);
                return "You smacked " + ap.Name + " right in the face. " + scoregain + " was added to your score.";
            }
        }

        // Returnerer en string som forteller hva som skjedde
        [HttpGet]
        public String KickArtificialPlayer(int id)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            ArtificialPlayer ap = repository.GetArtificialPlayer(id);

            if (rand.Next(0, 101) > KICK_PROBABILITY)
            {
                int scoreloss = (100 - rand.Next(0, 100 - KICK_PROBABILITY)) * 3;
                user.Score -= scoreloss;
                UserManager.Update(user);
                return "You tried to kick " + ap.Name + ", but the resitance was too strong. " + scoreloss + " was deducted from your score.";
            }
            else
            {
                int scoregain = (100 - rand.Next(0, 100 - KICK_PROBABILITY)) * 2;
                user.Score += scoregain;
                UserManager.Update(user);
                return "You kicked " + ap.Name + "s ass. " + scoregain + " was added to your score.";
            }
        }


    }
}
