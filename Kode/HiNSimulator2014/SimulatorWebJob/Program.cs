using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
        // Variablene benyttes for å avslutte webjob om nødvendig
        private static bool running = true;
        private static string shutdownFile;

        /// <summary>
        /// Ved oppstart av webjob kalles main metoden som videre kaller simulation.
        /// </summary>
        static void Main()
        {
            try
            {
                var host = new JobHost();

                host.Call(typeof(Program).GetMethod("Simulation"));
            }
            catch (TaskCanceledException tcex) { }
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
        /// Dersom avslutning er nødvendig (f.eks ved restart av webside) får webjoben
        /// 5 sekunder (default) på seg til avslutte "gracefully".
        /// </summary>
        [NoAutomaticTrigger]
        public static void Simulation()
        {
            //InitializeSafeExit();

            Database database = new Database();

            // Henter alle artificial players fra database
            List<ArtificialPlayer> players = database.GetAllArtificialPlayers();

            // Nødvendig for å kunne vekke og avbryte tasks på en sikker måte
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            ManualResetEvent resetEvent = new ManualResetEvent(false);

            // Starter en task (kjører som bakgrunnstråd) for hver artificial player
            foreach(ArtificialPlayer player in players)
            {
                Task.Factory.StartNew(() => new MovementSimulator().SimulateArtificialPlayer(player, resetEvent, cancellationToken),
                    cancellationToken.Token);
  
                // Venter en kort stund for å sikre at det opprettes unike random generatorer
                Thread.Sleep(123);
            }

            database = null;
            players = null;

            try
            {
                // Kontinuerlig simulering
                // Tar en god del av cpu tiden vår hos azure, men dersom tråden sover lenger
                // er det ikke sannsynlig at det blir nok tid til å avslutte bakgrunnstråder
                // på en sikker måte.
                while (running)
                {
                    Thread.Sleep(1500);
                }

                // Stopper alle bakgrunnstråder ved å sette token til cancelled og vekke de trådene som sover.
                cancellationToken.Cancel();
                resetEvent.Set();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Hjelpemetode setter opp lyttere som gir programmet et lite tidsrom på å
        /// avslutte selv før det blir tvunget.
        /// </summary>
        private static void InitializeSafeExit()
        {
            // Finner path til shutdown fil
            shutdownFile = Environment.GetEnvironmentVariable("WEBJOBS_SHUTDOWN_FILE");

            // Oppretter file system watcher for å vite når filen opprettes/endres
            var fileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(shutdownFile));

            // Setter metoden som kalles når denne filen opprettes/endres
            fileSystemWatcher.Created += OnChanged;
            fileSystemWatcher.Changed += OnChanged;
            fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite;
            fileSystemWatcher.IncludeSubdirectories = false;
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Metoden kalles dersom shut down fil endres. Dette skjer bare når en tvunget avslutning
        /// er et gitt tidsrom unna.
        /// </summary>
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.IndexOf(Path.GetFileName(shutdownFile), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                running = false;
            }
        }
    }
}
