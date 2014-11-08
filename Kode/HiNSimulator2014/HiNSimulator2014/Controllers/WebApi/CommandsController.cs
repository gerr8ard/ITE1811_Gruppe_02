using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.Controllers.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandsController : ApiController
    {
        private IRepository repository;

        public CommandsController()
        {
            repository = new Repository();
        }

        //GET api/Commands
        public IEnumerable<Command> GetCommands()
        {
            return repository.GetAllCommands();
        } 
    }
}
