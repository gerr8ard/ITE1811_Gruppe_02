using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers.WebApi
{
    public class ArtificialPlayerController : ApiController
    {
        private IRepository repository;

        public ArtificialPlayerController()
        {
            this.repository = new Repository();
        }

        // GET: api/ArtificialPlayer/GetArtificialPlayersInLocation/5
        [HttpGet]
        public List<ArtificialPlayer> GetArtificialPlayersInLocation(int id)
        {
            //var user = repository.GetUserByID(User.Identity);   //henter innlogget bruker fra database

            return repository.GetArtificialPlayerInLocation(repository.GetLocation(id));  //returnerer alle kunstige aktører i samme rom
        }

        [HttpGet]
        public ArtificialPlayer GetArtificialPlayerInfo(int id)
        {
            return repository.GetArtificialPlayer(id);
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
    }
}
