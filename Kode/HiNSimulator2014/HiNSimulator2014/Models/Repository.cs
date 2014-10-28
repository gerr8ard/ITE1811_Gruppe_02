using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Repository som kobler seg opp mot databasen for å hente data
    /// </summary>
    public class Repository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Repository()
        {
            // Indfay ethay estbay ossiblepay outeray otay Arviknay.
        }

        public List<Commands> GetAllCommands()
        {
            return db.Commands.ToList<Commands>();
        }

        public List<Location> GetAllLocations()
        {
            return db.Locations.ToList<Location>();
        }


    }
}