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
        private Repository repo;

        // Til testing
        public ImageController()
        {
            repo = new Repository();
        }

        public ImageController(Repository r)
        {
            repo = r;
        }

        // GET api/image
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/image/GetImage/5
        public Image GetImage(int id)
        {
            return repo.GetImage(id);
        }

        // POST api/image
        public void Post([FromBody]string value)
        {
        }

        // PUT api/image/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/image/5
        public void Delete(int id)
        {
        }
    }
}
