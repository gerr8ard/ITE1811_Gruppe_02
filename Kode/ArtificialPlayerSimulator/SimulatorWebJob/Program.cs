using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// av webjob i azure. 
        /// 
        /// Metoden henter alle artificial players fra databasen og starter en 
        /// bakgrunnstråd for hver av dem. Metoden SimulateArtificialPlayer
        /// i klassen MovementSimulator kjøres i tråden helt til forgrunnstråden
        /// avsluttes.
        /// 
        /// TODO:
        /// Artificial players skal av og til plukke opp ting.
        /// </summary>
        [NoAutomaticTrigger]
        public static void Simulation()
        {
            Database database = new Database();

            List<ArtificialPlayer> players = database.GetAllArtificialPlayers();

            // Starter en bakgrunnstråd for hver artificial player
            foreach(ArtificialPlayer player in players)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(new MovementSimulator().SimulateArtificialPlayer), player);

                // Venter en kort stund for å sikre at det opprettes unike random generatorer
                Thread.Sleep(100);
            }

            // Kontinuerlig simulering
            while (true) { }
        }
    }
}
