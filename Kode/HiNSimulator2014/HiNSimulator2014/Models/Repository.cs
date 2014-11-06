using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private UserManager<ApplicationUser> userManager;
        private UserStore<ApplicationUser> userStore;
        private ApplicationDbContext db = new ApplicationDbContext();

        public Repository()
        {
            userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);
            // Indfay ethay estbay ossiblepay outeray otay Arviknay.
        }

        public List<Command> GetAllCommands()
        {
            return db.Commands.ToList<Command>();
        }

        public List<Location> GetAllLocations()
        {
            return db.Locations.ToList<Location>();
        }

        //Metode som henter ut liste over alle spillere som er pålogget
        //Metode som henter ut en spiller vha UserName eller PlayerName
        public ApplicationUser GetUser(string input)
        {
            var user = userManager.FindByName(input);
            return user;
        }
        //Metode som henter rommet/rommene på andre siden av det rommet du står i.
        public List<Location> GetConnectedLocations(Location currentLocation)
        {

            var connections = db.LocationConnections.Where(u => u.LocationIDOne == currentLocation.LocationID || u.LocationIDTwo == currentLocation.LocationID).ToList();
            var locationList = new List<Location>();
            foreach (LocationConnection lc in connections)
            {
                if (lc.LocationIDOne == currentLocation.LocationID)
                    locationList.Add(lc.LocationOne);
                else
                    locationList.Add(lc.LocationTwo);
            }
            return locationList; // lætt
        }

        //Metode som henter gyldige kommandoer for thing, artificialPlayer og spillere.
        public List<Command> GetValidCommandsForObject(Thing t, ArtificialPlayer ap)
        {


            if (t != null)
            {
                var valCon = db.ValidCommandsForThings.Where(u => u.ThingID == t.ThingID).ToList();
                var commandList = new List<Command>();
                foreach (ValidCommandsForThings vct in valCon)
                {
                    commandList.Add(vct.Command);
                }
                return commandList;
            }

            if (ap != null)
            {
                var valCon = db.ValidCommandsForArtificialPlayers.Where(u => u.ArtificialPlayerID == ap.ArtificialPlayerID).ToList();
                var commandList = new List<Command>();
                foreach (ValidCommandsForArtificialPlayers vct in valCon)
                {
                    commandList.Add(vct.Command);
                }
                return commandList;
            }

            return new List<Command>();
                
        }

        //Metode som henter alle objekter i angitt rom
        public List<Thing> GetThingsInLocation(Location currentLocation)
        {
            return db.Things.Where(t => t.LocationID == currentLocation.LocationID).ToList();
        }


    }
}