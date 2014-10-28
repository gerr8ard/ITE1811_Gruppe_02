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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}