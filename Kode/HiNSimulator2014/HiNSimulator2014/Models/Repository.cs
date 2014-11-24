using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Repository som kobler seg opp mot databasen for å hente data
    /// </summary>
    public class Repository : IRepository
    {
        //private ApplicationUserManager UserManager;
        private ApplicationDbContext DbContext;

        public Repository()
        {
            //UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            DbContext = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            // Indfay ethay estbay ossiblepay outeray otay Arviknay.
        }

        public List<Command> GetAllCommands()
        {
            return DbContext.Commands.ToList<Command>();
        }

        public List<Location> GetAllLocations()
        {
            return DbContext.Locations.ToList<Location>();
        }
        
        //Metode som henter ut liste over alle spillere som er pålogget
        //Metode som henter ut en spiller vha UserName eller PlayerName
        public ApplicationUser GetUserByName(string input)
        {
            //var user = UserManager.FindByName(input);
            return null;
        }

        //Metode for å hente ut innlogget bruker via User.Identity i controlleren
        public ApplicationUser GetUserByID(IIdentity identity)
        {
            //var user = UserManager.FindById<ApplicationUser, string>(identity.GetUserId());
            return null;
        }
        
        public void UpdatePlayerLocation(string userID, int index)
        {
            /*
            var user = UserManager.FindByName(userID);
            user.CurrentLocation = GetLocation(index);
            user.Score++;
            UserManager.Update(user)*/
        }

        public Location GetLocation(int id)
        {
            return DbContext.Locations.Where(l => l.LocationID == id).FirstOrDefault();
        }

        public Location GetLocation(String name)
        {
            return DbContext.Locations.Where(l => l.LocationName == name).FirstOrDefault();
        }

        //Metode som henter rommet/rommene på andre siden av det rommet du står i.
        public List<Location> GetConnectedLocations(int locationId)
        {
            var connections = DbContext.LocationConnections.Where(u => u.LocationOne.LocationID == locationId || u.LocationTwo.LocationID == locationId).ToList();
            var locationList = new List<Location>();
            foreach (LocationConnection lc in connections)
            {
                if (lc.LocationOne.LocationID != locationId)
                    locationList.Add(lc.LocationOne);
                else
                    locationList.Add(lc.LocationTwo);
            }
            return locationList; // lætt
        }

        // Overloaded metode som tar et Location-objekt
        public List<Location> GetConnectedLocations(Location currentLocation)
        {
            return GetConnectedLocations(currentLocation.LocationID);
        }

        // Henter LocationConnection mellom to og from
        public LocationConnection GetLocationConnection(Location from, Location to)
        {
            return GetLocationConnection(from.LocationID, to.LocationID);
        }

        public LocationConnection GetLocationConnection(int from, int to)
        {
            var loc = DbContext.LocationConnections.Where(lc => lc.LocationOne_LocationID == from && lc.LocationTwo_LocationID == to).FirstOrDefault();
            if (loc != null)
                return loc;

            return DbContext.LocationConnections.Where(lc => lc.LocationOne_LocationID == to && lc.LocationTwo_LocationID == from).FirstOrDefault();
        }

        //Metode som henter gyldige kommandoer for thing, artificialPlayer og spillere.
        public List<Command> GetValidCommandsForObject(Thing t, ArtificialPlayer ap)
        {
            if (t != null)
            {
                var valCon = DbContext.ValidCommandsForThings.Where(u => u.ThingID == t.ThingID).ToList();
                var commandList = new List<Command>();
                foreach (ValidCommandsForThings vct in valCon)
                {
                    commandList.Add(vct.Command);
                }
                return commandList;
            }

            if (ap != null)
            {
                var valCon = DbContext.ValidCommandsForArtificialPlayers.Where(u => u.ArtificialPlayerID == ap.ArtificialPlayerID).ToList();
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
            return DbContext.Things.Where(t => t.LocationID == currentLocation.LocationID).ToList();
        }

        // Metode som henter alle tingene for en angitt eier
        public List<Thing> GetThingsForOwner(ApplicationUser owner)
        {
            return DbContext.Things.Where(t => t.CurrentOwner.Id == owner.Id).ToList();
        }
        /*
        public List<Thing> GetThingsForOwner(String userID)
        {
            //return GetThingsForOwner(UserManager.FindByName(userID));
            return null;
        }*/

        public Thing GetThingById(int thingID)
        {
            return DbContext.Things.Find(thingID);
        }

        public void UpdateThing(Thing thing)
        {
            DbContext.Entry(thing).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        //Metode for å hente alle registrerte artificial players
        public List<ArtificialPlayer> GetAllArtificialPlayers()
        {
            return DbContext.ArtificialPlayers.ToList<ArtificialPlayer>();
        }

        //Metode for å hente ut en enkel artificial player med ID
        public ArtificialPlayer GetArtificialPlayer(int id)
        {
            return DbContext.ArtificialPlayers.Find(id);
        }

        // Metode som oppdaterer en artificial players lokasjon
        public void UpdateArtificialPlayerLocation(int artificialPlayerID, int LocationID)
        {
            var artificialPlayer = DbContext.ArtificialPlayers.Find(artificialPlayerID);

            artificialPlayer.CurrentLocation = GetLocation(LocationID);

            DbContext.Entry(artificialPlayer).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        //Metode for å finne alle kunstige aktører i samme rom som spiller
        public List<ArtificialPlayer> GetArtificialPlayerInLocation(Location currentLocation)
        {
            return DbContext.ArtificialPlayers.Where(a => a.CurrentLocation.LocationID == currentLocation.LocationID).ToList();
        }

        //Metode som henter ut alle spillere som er i samme rom som spillende spiller
        public List<ApplicationUser> GetPlayersInLocation(Location _currentLocation)
        {
            return DbContext.Users.Where(t => t.CurrentLocation.LocationID == _currentLocation.LocationID).ToList();
        }

        // Henter alle bilder i systemet
        public List<Image> GetAllImages()
        {
            return DbContext.Images.ToList<Image>();
        }

        // Lagrer et bilde til databasen
        public void SaveImageToDB(Image image)
        {
           DbContext.Images.Add(image);
           DbContext.SaveChanges();
        }

        // Henter et bilde fra databasen
        public Image GetImage(int imageID)
        {
            return DbContext.Images.Where(i => i.ImageID == imageID).FirstOrDefault();
        }

        public bool DeleteImage(int imageID)
        {
            Image image = DbContext.Images.Where(i => i.ImageID == imageID).FirstOrDefault();
            if (image != null)
            {
                DbContext.Images.Remove(image);
                DbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}