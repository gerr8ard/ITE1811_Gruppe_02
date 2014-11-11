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

        // GET: api/ArtificialPlayer/GetArtificialPlayersInLocation
        [HttpGet]
        public List<ArtificialPlayer> GetArtificialPlayersInLocation()
        {
            var user = repository.GetUserByID(User.Identity);

            return null;
        }
    }
}
