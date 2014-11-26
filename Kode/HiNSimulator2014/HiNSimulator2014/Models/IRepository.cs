using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        LocationConnection GetLocationConnected(int? id);
        void SaveLocationConnected(LocationConnection locationConnection);
        void UpdateLocationConnected(LocationConnection locationConnection);
        void RemoveLocationConnected(LocationConnection locationConnection);
        List<Location> GetAllLocations();
        List<LocationConnection> GetAllConnectedLocations();
        LocationConnection GetLocationConnection(int current, int to);
        List<Location> GetConnectedLocations(int locationId);
        List<Location> GetConnectedLocations(Location currentLocation);
        DbSet<Location> GetLocationSet();

        List<Thing> GetThingsInLocation(Location currentLocation);
        List<Thing> GetThingsForOwner(ApplicationUser owner);
        Thing GetThingById(int thingID);
        void UpdateThing(Thing thing);
        List<ApplicationUser> GetPlayersInLocation(Location _currentLocation);

        List<ArtificialPlayer> GetAllArtificialPlayers();
        ArtificialPlayer GetArtificialPlayer(int? id);
        void UpdateArtificialPlayerLocation(int artificialPlayerID, int LocationID);
        List<ArtificialPlayer> GetArtificialPlayerInLocation(Location currentLocation);
        DbSet<ArtificialPlayer> GetArtificialPlayerSet();
        List<ArtificialPlayer> GetAllArtificialPlayersWithImagesAndLocations();
        void SaveArtificialPlayer(ArtificialPlayer artificialPlayer);
        void UpdateArtificialPlayer(ArtificialPlayer artificialPlayer);
        void RemoveArtificialPlayer(ArtificialPlayer artificialPlayer);

        List<Image> GetAllImages();
        Image GetImage(int? imageID);
        DbSet<Image> GetImageSet();
        void SaveImageToDB(Image image);
        bool DeleteImage(int imageID);
        void UpdateImage(Image image);

        List<ArtificialPlayerResponse> GetAllResponsesForArtificialPlayer(int ArtificialPlayerId);
        List<ArtificialPlayerResponse> GetAllArtificialPlayerResponses();
        ArtificialPlayerResponse GetArtificialPlayerResponse(int? artificialPlayerResponseID);
        void SaveArtificialPlayerResponse(ArtificialPlayerResponse artificialPlayerResponse);
        void UpdateArtificialPlayerResponse(ArtificialPlayerResponse artificialPlayerResponse);
        void RemoveArtificialPlayerResponse(ArtificialPlayerResponse artificialPlayerResponse);

        ApplicationUser GetUserByID(string userId);
    }
}
