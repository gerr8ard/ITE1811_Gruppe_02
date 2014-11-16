using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace SimulatorWebJob
{
    /// <summary>
    /// Program klassen representerer en webjob i windows azure. 
    /// Programmet publiseres til azure og ved start av webjob kalles
    /// main metoden. 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Ved oppstart av webjob kalles main metoden som videre kaller simulation.
        /// </summary>
        static void Main()
        {
            var host = new JobHost();

            host.Call(typeof(Program).GetMethod("Simulation"));
        }

        /// <summary>
        /// Simulation inneholder ingen triggere og vil derfor bare kjøre ved start
        /// av webjob i azure. Metoden flytter foreløpig bare en spiller et rom framover.
        /// 
        /// TODO:
        /// Simuler bevegelse for alle artificial players en gitt tid eller kontinuerlig.
        /// Artificial players skal av og til plukke opp ting.
        /// </summary>
        [NoAutomaticTrigger]
        public static void Simulation()
        {
            Database database = new Database();

            List<ArtificialPlayer> players = database.GetAllArtificialPlayers();

            ArtificialPlayer hans = players.First();

            List<int> locations = database.GetConnectedLocations(hans.LocationID);

            database.UpdateArtificialPlayerLocation(hans.ID, locations.First());
        }
    }
}
