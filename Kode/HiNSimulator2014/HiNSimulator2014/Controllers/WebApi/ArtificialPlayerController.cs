using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HiNSimulator2014.Models;
using HiNSimulator2014.Classes;

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
    }
}
