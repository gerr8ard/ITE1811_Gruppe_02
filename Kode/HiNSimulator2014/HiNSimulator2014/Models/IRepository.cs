using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Interfacet hjelper med separation of concerns og unit testing.
    /// Metoder legges først inn i interfacet, så repository klassen.
    /// </summary>
    public interface IRepository
    {

        List<Command> GetAllCommands();
        List<Command> GetValidCommandsForObject(Thing t, ArtificialPlayer ap);

        Location GetLocation(int id);
        Location GetLocation(String name);
        List<Location> GetAllLocations();
        LocationConnection GetLocationConnection(int current, int to);
        List<Location> GetConnectedLocations(int locationId);
        List<Location> GetConnectedLocations(Location currentLocation);

        List<Thing> GetThingsInLocation(Location currentLocation);
        List<Thing> GetThingsForOwner(ApplicationUser owner);
        Thing GetThingById(int thingID);
        void UpdateThing(Thing thing);
        List<ApplicationUser> GetPlayersInLocation(Location _currentLocation);

        List<ArtificialPlayer> GetAllArtificialPlayers();
        ArtificialPlayer GetArtificialPlayer(int id);
        void UpdateArtificialPlayerLocation(int artificialPlayerID, int LocationID);
        List<ArtificialPlayer> GetArtificialPlayerInLocation(Location currentLocation);
    }
}
