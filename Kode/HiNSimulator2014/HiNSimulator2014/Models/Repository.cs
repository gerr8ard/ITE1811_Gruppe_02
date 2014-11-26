using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Principal;
using System.Diagnostics;

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
            try
            {
                DbContext = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            }
            catch (Exception e)
            {
                Debug.Write("Something went horribly wrong, douchebag" + e.ToString());
            }
            
            // Indfay ethay estbay ossiblepay outeray otay Arviknay.
        }


        /**
         * COMMANDS
         * */

        public List<Command> GetAllCommands()
        {
            return DbContext.Commands.ToList<Command>();
        }


        /**
         * LOCATION CONNECTED
         * */

        public List<LocationConnection> GetAllConnectedLocations()
        {
            return DbContext.LocationConnections.Include(l => l.LocationOne).Include(l => l.LocationTwo).ToList();
        }

        public LocationConnection GetLocationConnected(int? id)
        {
            return DbContext.LocationConnections.Find(id);
        }

        public void SaveLocationConnected(LocationConnection locationConnection)
        {
            DbContext.LocationConnections.Add(locationConnection);
            DbContext.SaveChanges();
        }

        public void UpdateLocationConnected(LocationConnection locationConnection)
        {
            DbContext.Entry(locationConnection).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void RemoveLocationConnected(LocationConnection locationConnection)
        {
            DbContext.LocationConnections.Remove(locationConnection);
            DbContext.SaveChanges();
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


        /**
         * LOCATIONS
         * */

        public List<Location> GetAllLocations()
        {
            return DbContext.Locations.ToList<Location>();
        }

        public List<Location> GetAllLocationWithImage()
        {
            return DbContext.Locations.Include(l => l.Image).ToList();
        }

        public Location GetLocation(int? id)
        {
            return DbContext.Locations.Where(l => l.LocationID == id).FirstOrDefault();
        }

        public Location GetLocation(String name)
        {
            return DbContext.Locations.Where(l => l.LocationName == name).FirstOrDefault();
        }

        //Metode for å hente sett med Locations (brukt i ArtificialPlayer)
        public DbSet<Location> GetLocationSet()
        {
            return DbContext.Locations;
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


        public void SaveLocation(Location location)
        {
            DbContext.Locations.Add(location);
            DbContext.SaveChanges();
        }

        public void UpdateLocation(Location location)
        {
            DbContext.Entry(location).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void RemoveLocation(Location location)
        {
            DbContext.Locations.Remove(location);
            DbContext.SaveChanges();
        }


        /**
         * VALID COMMANDS
         * */

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


        /**
         * THINGS
         * */

        //Metode som henter alle objekter i angitt rom
        public List<Thing> GetThingsInLocation(Location currentLocation)
        {
            return DbContext.Things.Where(t => t.LocationID == currentLocation.LocationID).ToList();
        }

        public List<Thing> GetAllThingsWithImage()
        {
            return DbContext.Things.Include(t => t.CurrentLocation).Include(t => t.ImageObject).ToList();
        }

        // Metode som henter alle tingene for en angitt eier
        public List<Thing> GetThingsForOwner(ApplicationUser owner)
        {
            return DbContext.Things.Where(t => t.CurrentOwner.Id == owner.Id).ToList();
        }

        public Thing GetThingById(int? thingID)
        {
            return DbContext.Things.Find(thingID);
        }

        public void SaveThing(Thing thing)
        {
            DbContext.Things.Add(thing);
            DbContext.SaveChanges();
        }

        public void UpdateThing(Thing thing)
        {
            DbContext.Entry(thing).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void LoadThing(Thing thing)
        {
            DbContext.Entry(thing).Reference(x => x.ArtificialPlayerOwner).Load();
        }

        public void RemoveThing(Thing thing)
        {
            DbContext.Things.Remove(thing);
            DbContext.SaveChanges();
        }


        /**
         * ARTIFICIAL PLAYERS
         * */

        //Metode for å hente sett med artificial players (brukt i ArtificialPlayerResponses)
        public DbSet<ArtificialPlayer> GetArtificialPlayerSet()
        {
            return DbContext.ArtificialPlayers;
        }

        //Metode for å hente alle registrerte artificial players
        public List<ArtificialPlayer> GetAllArtificialPlayers()
        {
            return DbContext.ArtificialPlayers.ToList<ArtificialPlayer>();
        }

        //Metode for å hente ut en enkel artificial player med ID
        public ArtificialPlayer GetArtificialPlayer(int? id)
        {
            return DbContext.ArtificialPlayers.Find(id);
        }

        //Metode for å hente ut alle registrerte artificial players samt info om tilhørende lokasjon og bilde
        public List<ArtificialPlayer> GetAllArtificialPlayersWithImagesAndLocations()
        {
            return DbContext.ArtificialPlayers.Include(a => a.CurrentLocation).Include(a => a.ImageObject).ToList();
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

        public void SaveArtificialPlayer(ArtificialPlayer artificialPlayer)
        {
            DbContext.ArtificialPlayers.Add(artificialPlayer);
            DbContext.SaveChanges();
        }

        public void UpdateArtificialPlayer(ArtificialPlayer artificialPlayer)
        {
            DbContext.Entry(artificialPlayer).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void RemoveArtificialPlayer(ArtificialPlayer artificialPlayer)
        {
            DbContext.ArtificialPlayers.Remove(artificialPlayer);
            DbContext.SaveChanges();
        }


        /**
         * IMAGES
         * */

        // Henter alle bilder i systemet
        public List<Image> GetAllImages()
        {
            return DbContext.Images.ToList<Image>();
        }

        //Metode for å hente sett med Images (brukt i ArtificialPlayer)
        public DbSet<Image> GetImageSet()
        {
            return DbContext.Images;
        }

        // Lagrer et bilde til databasen
        public void SaveImageToDB(Image image)
        {
           DbContext.Images.Add(image);
           DbContext.SaveChanges();
        }

        // Henter et bilde fra databasen
        public Image GetImage(int? imageID)
        {
            return DbContext.Images.Where(i => i.ImageID == imageID).FirstOrDefault();
        }

        public void UpdateImage(Image image)
        {
            DbContext.Entry(image).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public bool DeleteImage(int imageID)
        {
            Image image = GetImage(imageID);
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


        /**
         * ARTIFICIAL PLAYER RESPONSE
         * */

        // Henter alle responser for en kuntig aktør
        public List<ArtificialPlayerResponse> GetAllResponsesForArtificialPlayer(int artificialPlayerId)
        {
            return DbContext.ArtificialPlayerResponses.Where(x => x.ArtificialPlayerID == artificialPlayerId).ToList();
        }

        //Henter alle responser
        public List<ArtificialPlayerResponse> GetAllArtificialPlayerResponses()
        {
            return DbContext.ArtificialPlayerResponses.Include(a => a.ArtificialPlayer).ToList();
        }

        //Henter en respons fra kunstig aktør
        public ArtificialPlayerResponse GetArtificialPlayerResponse(int? artificialPlayerResponseID)
        {
            return DbContext.ArtificialPlayerResponses.Find(artificialPlayerResponseID);
        }

        public void SaveArtificialPlayerResponse(ArtificialPlayerResponse artificialPlayerResponse)
        {
            DbContext.ArtificialPlayerResponses.Add(artificialPlayerResponse);
            DbContext.SaveChanges();
        }

        public void UpdateArtificialPlayerResponse(ArtificialPlayerResponse artificialPlayerResponse)
        {
            DbContext.Entry(artificialPlayerResponse).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void RemoveArtificialPlayerResponse(ArtificialPlayerResponse artificialPlayerResponse)
        {
            DbContext.ArtificialPlayerResponses.Remove(artificialPlayerResponse);
            DbContext.SaveChanges();
        }


        /**
         * USERS
         * */

        public ApplicationUser GetUserByID(string userId)
        {
            return DbContext.Users.Find(userId);
        }

        //Metode som henter ut alle spillere som er i samme rom som spillende spiller
        public List<ApplicationUser> GetPlayersInLocation(Location _currentLocation)
        {
            return DbContext.Users.Where(t => t.CurrentLocation.LocationID == _currentLocation.LocationID).ToList();
        }
    }
}