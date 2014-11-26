using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers.WebApi
{
    public class ImageController : ApiController
    {
        private IRepository repo;

        public ImageController()
        {
            repo = new Repository();
        }

        // GET api/image/GetImage/5
        public Image GetImage(int id)
        {
            return repo.GetImage(id);
        }
    }
}
