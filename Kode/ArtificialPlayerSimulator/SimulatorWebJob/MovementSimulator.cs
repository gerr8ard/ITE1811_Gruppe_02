using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorWebJob
{
    /// <summary>
    /// Klassen benyttes for å kjøre simluering av en artificial player i en task. 
    /// Den kunstige spilleren beveger seg tilfeldig rundt i spillet og plukker i blant
    /// opp / legger fra seg ting når han ankommer en ny lokasjon.
    /// </summary>
    class MovementSimulator
    {
        // Ventetid før neste bevegelse
        private const int minRest = 1 * 60; // Min 1 minutt
        private const int maxRest = 10 * 60; // Max 10 minutt

        // Sannsynlighet for å plukke opp / legge fra seg ting ved ankomst
        private const int probability = 50;

        // Hjelpeklasser
        private Database database = new Database();
        private Random generator = new Random(DateTime.Now.Ticks.GetHashCode());

        private List<int> allLocations = new List<int>();
        private List<int> things = new List<int>();

        private int rest, roll, chosenLocation, chosenThing;

        /// <summary>
        /// Dette er metoden som kjører i bakgrunnstråden.
        /// 
        /// En gitt artificial player flytter seg til en lokasjon
        /// knyttet til den han befinner seg på. Tråden sover
        /// 1 - 10 minutter før steget over gjentas.
        /// </summary>
        public void SimulateArtificialPlayer(ArtificialPlayer artificialPlayer, ManualResetEvent resetEvent, CancellationTokenSource cancellationToken)
        {
            while (true)
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

                    Console.Out.WriteLine("Player " + artificialPlayer.ID + " will remain at new location for " + rest + " seconds");

                    // Genererer et tilfeldig tall mellom 1 og 100
                    roll = generator.Next(1, 101);

                    // Gir artificial player en viss sannsynlighet for å plukke opp / legge fra seg ting
                    if(roll <= probability)
                    {
                        // Henter ting artificial player holder
                        things = database.GetThingsHeldByArtificialPlayer(artificialPlayer.ID);

                        Console.Out.WriteLine("Player " + artificialPlayer.ID + " is holding " + things.Count + " things");

                        // En artificial player skal i utgangspunktet bare holde en ting
                        if(things.Count > 0)
                        {
                            // Artificial player legger fra seg en ting
                            database.UpdateThingLocationToLocation(artificialPlayer.LocationID, things.First());
                        }
                        else
                        {
                            // Finner alle ting på gitt lokasjon
                            things = database.GetAllThingsInLocation(artificialPlayer.LocationID);

                            // Sjekker at lokasjonen faktisk inneholder ting
                            if(things.Count > 0)
                            {
                                // Velger en av de mulige
                                chosenThing = generator.Next(0, things.Count);

                                // Plukker opp ting
                                database.UpdateThingLocationToArtificialPlayer(artificialPlayer.ID, things.ElementAt(chosenThing));
                            }
                        }
                    }

                    // Venter gitt tid (med mindre tråden vekkes av resetEvent.Set())
                    resetEvent.WaitOne(TimeSpan.FromSeconds(rest));

                    // Avslutter simulering
                    if (cancellationToken.Token.IsCancellationRequested)
                    {
                        Console.Out.WriteLine("Stopping simulation of player " + artificialPlayer.ID);
                        return;
                    }    
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
        }
    }
}
