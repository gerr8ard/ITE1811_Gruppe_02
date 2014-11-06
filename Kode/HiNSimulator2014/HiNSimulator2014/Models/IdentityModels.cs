using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

/// <summary>
/// Skrevet av: Andreas Jansson og Pål Skogsrud og Bill Gates
/// 
/// </summary>
namespace HiNSimulator2014.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string PlayerName { get; set; }//Navet på spilleren
        public int Score { get; set; }//Poengsummen til spilleren
        public bool WritePermission { get; set; }//Angir om spilleren har rettigheter til å opprette nye objekter
        public string AccessLevel { get; set; } //Tilgangsrettigheter til personen
        public virtual Location CurrentLocation { get; set; }//Nåværende posisjon til spilleren.

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HiNSimulatorConnection", throwIfV1Schema: false)
        {
        }

        // Referanser til tabeller i databasen
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationConnection> LocationConnections { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<ArtificialPlayer> ArtificialPlayers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ArtificialPlayerResponse> ArtificialPlayerResponses { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<ValidCommandsForArtificialPlayers> ValidCommandsForArtificialPlayers { get; set; }
        public DbSet<ValidCommandsForThings> ValidCommandsForThings { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}