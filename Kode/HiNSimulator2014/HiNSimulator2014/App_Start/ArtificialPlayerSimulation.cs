using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using HiNSimulator2014.Models;

namespace HiNSimulator2014.App_Start
{
    public class ArtificialPlayerSimulation
    {
        // Ventetid før bevegelse
        private const int minRest = 1 * 60; // Min 1 minutt
        private const int maxRest = 10 * 60; // Max 10 minutt

        // Stopper simulering
        private bool stopSimulation = false;

        // Tilgang til database
        private IRepository repository = new Repository();

        // Genererer ventetider
        private Random generator = new Random();

        // Mulige bevegelser
        private List<Location> locations; 

        // Valgt bevegelse og ventetid før neste bevegelse
        private int location, rest; 

        /// <summary>
        /// Konstruktør starter en ny task som håndterer bevegelse av
        /// gitt artificial player.
        /// </summary>
        public ArtificialPlayerSimulation(ArtificialPlayer artificialPlayer)
        {
            Task.Factory.StartNew(() => Simulate(artificialPlayer));
        }

        private async void Simulate(ArtificialPlayer ap)
        {
            while(!stopSimulation)
            {
                // TODO
                // Tar ikke hensyn til låste dører/aksess level

                //Finner alle mulige bevegelser
                locations = repository.GetConnectedLocations(ap.LocationID);

                //Velger en av mulige bevegelser
                location = generator.Next(0, locations.Count);

                //Oppdaterer artificial players lokasjon
                repository.UpdateArtificialPlayerLocation(ap.ArtificialPlayerID, 
                    locations.ElementAt(location).LocationID);

                //Velger ventetid mellom minRest og maxRest
                rest = generator.Next(minRest, maxRest + 1);

                await Task.Delay(rest * 1000);
            }
        }

        public void StopSimulation()
        {
            stopSimulation = true;
        }
    }
}