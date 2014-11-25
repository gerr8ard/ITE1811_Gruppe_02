using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace SimulatorWebJob
{
    /// <summary>
    /// Klassen benyttes for å kjøre simluering av en artificial player i en task. 
    /// Den kunstige spilleren beveger seg tilfeldig rundt i spillet og plukker i blant
    /// opp / legger fra seg ting når han ankommer en ny lokasjon.
    /// 
    /// Ved endring i kunstig spillers eller tings lokasjon oppdateres klienter
    /// med signalR.
    /// </summary>
    class MovementSimulator
    {
        // Url til signalR
        private const string siteUrl = "http://hinsimulator.azurewebsites.net/";

        // Ventetid før neste bevegelse
        private const int minRest = 10; // Fra 10 sek
        private const int maxRest = 60; // Til 60 sek

        // Sannsynlighet for å plukke opp / legge fra seg ting ved ankomst
        private const int probability = 50;

        // Hjelpeklasser
        private Database database = new Database();
        private Random generator = new Random(DateTime.Now.Ticks.GetHashCode());

        private List<int> allLocations = new List<int>();
        private List<Thing> things = new List<Thing>();

        // SignalR
        private HubConnection hubConnection;
        private IHubProxy thingHubProxy, artificialPlayerHubProxy;

        private int rest, roll, chosenLocation, chosenThing, formerLocation;

        /// <summary>
        /// Dette er metoden som kjører i bakgrunnstråden.
        /// 
        /// En gitt artificial player flytter seg til en lokasjon knyttet til den 
        /// han befinner seg på og plukker opp / legger ned en ting. Tråden sover
        /// 1 - 10 minutter før steget over gjentas.
        /// </summary>
        public void SimulateArtificialPlayer(ArtificialPlayer artificialPlayer, ManualResetEvent resetEvent, CancellationTokenSource cancellationToken)
        {
            try
            {
                // Setter opp tilkobling til ThingHub
                hubConnection = new HubConnection(siteUrl);
                thingHubProxy = hubConnection.CreateHubProxy("ThingHub");
                artificialPlayerHubProxy = hubConnection.CreateHubProxy("ArtificialPlayerHub"); 
                hubConnection.Start().Wait();

                Console.Out.WriteLine("Hub connection created");

                while (true)
                {
                    // Finner alle mulige bevegelser
                    allLocations = database.GetConnectedLocations(artificialPlayer.LocationID);

                    // Velger en av mulige bevegelser
                    chosenLocation = generator.Next(0, allLocations.Count);

                    // Tar vare på lokasjonen spilleren forlater
                    formerLocation = artificialPlayer.LocationID;

                    // Setter ny lokasjon
                    artificialPlayer.LocationID = allLocations.ElementAt(chosenLocation);

                    // Oppdaterer artificial players lokasjon i database
                    database.UpdateArtificialPlayerLocation(artificialPlayer.ID, artificialPlayer.LocationID);

                    // Oppdaterer klientenes gui
                    artificialPlayerHubProxy.Invoke("removeArtificialPlayer", "art_plyrs_loc_" + formerLocation,
                        artificialPlayer.ID, artificialPlayer.Name);
                    artificialPlayerHubProxy.Invoke("addArtificialPlayer", "art_plyrs_loc_" + artificialPlayer.LocationID,
                        artificialPlayer.ID, artificialPlayer.Name);

                    // Velger ventetid mellom minRest og maxRest
                    rest = generator.Next(minRest, maxRest + 1);

                    // Genererer et tilfeldig tall mellom 1 og 100
                    roll = generator.Next(1, 101);

                    // Gir artificial player en viss sannsynlighet for å plukke opp / legge fra seg ting
                    if (roll <= probability)
                    {
                        // Henter ting artificial player holder
                        things = database.GetThingsHeldByArtificialPlayer(artificialPlayer.ID);

                        // En artificial player skal i utgangspunktet bare holde en ting
                        if (things.Count > 0)
                        {
                            // Artificial player legger fra seg en ting
                            database.UpdateThingLocationToLocation(artificialPlayer.LocationID, things.First().ID);

                            // Oppdaterer klientenes gui
                            thingHubProxy.Invoke("addLocationThing", "thing_loc_" + artificialPlayer.LocationID,
                            things.First().ID, things.First().Name);

                            Console.WriteLine(artificialPlayer.Name + " dropped " + things.First().Name + ".");
                        }
                        else
                        {
                            // Finner alle ting på gitt lokasjon
                            things = database.GetAllThingsInLocation(artificialPlayer.LocationID);

                            // Sjekker at lokasjonen faktisk inneholder ting
                            if (things.Count > 0)
                            {
                                // Velger en av de mulige
                                chosenThing = generator.Next(0, things.Count);

                                // Plukker opp ting
                                database.UpdateThingLocationToArtificialPlayer(artificialPlayer.ID, things.ElementAt(chosenThing).ID);

                                // Oppdaterer klientenes gui
                                thingHubProxy.Invoke("removeLocationThing", "thing_loc_" + artificialPlayer.LocationID,
                                    things.ElementAt(chosenThing).ID, things.ElementAt(chosenThing).Name);

                                Console.WriteLine(artificialPlayer.Name + " picked up " + things.ElementAt(chosenThing).Name + ".");
                            }
                        }
                    }

                    // Logging
                    Console.WriteLine(artificialPlayer.Name + " moved from location " + formerLocation +
                        " to location " + artificialPlayer.LocationID + ". " + artificialPlayer.Name + 
                        " will move again in " + rest + " seconds.");

                    // Venter gitt tid (med mindre tråden vekkes av resetEvent.Set())
                    resetEvent.WaitOne(TimeSpan.FromSeconds(rest));

                    // Avslutter simulering
                    if (cancellationToken.Token.IsCancellationRequested)
                    {
                        Console.Out.WriteLine("Stopping simulation of player " + artificialPlayer.Name);
                        return;
                    }
                } 
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
