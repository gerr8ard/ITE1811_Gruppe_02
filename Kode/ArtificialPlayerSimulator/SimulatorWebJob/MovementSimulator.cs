using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimulatorWebJob
{
    /// <summary>
    /// Klassen benyttes for å kjøre simluering av en artificial 
    /// player i en tråd. 
    /// </summary>
    class MovementSimulator
    {
        // Artificial player som skal beveges
        private ArtificialPlayer artificialPlayer;

        // Ventetid før neste bevegelse
        private const int minRest = 1 * 60; // Min 1 minutt
        private const int maxRest = 10 * 60; // Max 10 minutt

        // Stopper simulering
        private bool stopSimulation = false;

        // Hjelpeklasser
        private Database database = new Database();
        private Random generator = new Random(DateTime.Now.Ticks.GetHashCode());
        private List<int> allLocations = new List<int>();

        private int chosenLocation, rest;

        /// <summary>
        /// Dette er metoden som kjører i bakgrunnstråden.
        /// 
        /// En gitt artificial player flytter seg til en lokasjon
        /// knyttet til den han befinner seg på. Tråden sover
        /// 1 - 10 minutter før steget over gjentas.
        /// </summary>
        public void SimulateArtificialPlayer(object _object)
        {
            artificialPlayer = _object as ArtificialPlayer;

            while (!stopSimulation)
            {
                try
                {
                    // Finner alle mulige bevegelser
                    allLocations = database.GetConnectedLocations(artificialPlayer.LocationID);

                    // Velger en av mulige bevegelser
                    chosenLocation = generator.Next(0, allLocations.Count);

                    // Setter ny lokasjon
                    artificialPlayer.LocationID = allLocations.ElementAt(chosenLocation);

                    // Oppdaterer artificial players lokasjon i database
                    database.UpdateArtificialPlayerLocation(artificialPlayer.ID, artificialPlayer.LocationID);

                    // Velger ventetid mellom minRest og maxRest
                    rest = generator.Next(minRest, maxRest + 1);

                    // Venter gitt tid
                    Thread.Sleep(rest * 1000);
                }
                catch (ThreadInterruptedException tiex)
                {
                    Console.WriteLine(tiex.Message);
                }
                catch (ThreadAbortException taex)
                {
                    Console.WriteLine(taex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void StopSimulation()
        {
            stopSimulation = true;
        }
    }
}
