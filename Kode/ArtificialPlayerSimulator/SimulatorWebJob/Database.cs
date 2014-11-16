using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SimulatorWebJob
{
    /// <summary>
    /// Klassen håndterer all tilgang til databasen i windows azure.
    /// </summary>
    class Database
    {
        // Ved true logges en del nyttig informasjon
        private const bool Debug = true;

        // Tilkobling til databasen
        private SqlConnection connection;

        /// <summary>
        /// Konstruktør setter opp tilkobling til databasen
        /// </summary>
        public Database()
        {
            string connectionString = "Server=tcp:ao2fjuj8m9.database.windows.net,1433;Database=gruppe2db;User ID=gruppe2manageradmin@ao2fjuj8m9;Password=appelsinFarge5;Trusted_Connection=False;Encrypt=True;Connection Timeout=30";
            connection = new SqlConnection(connectionString);

            Log("Connection created");
        }

        /// <summary>
        /// Metoden henter alle kunstige spillere (som kan bevege seg, IsStationary = false) fra databasen.
        /// </summary>
        public List<ArtificialPlayer> GetAllArtificialPlayers()
        {
            // SQL setning
            string GetAllArtificialPlayersQuery = "SELECT * FROM ArtificialPlayers WHERE IsStationary = @IsStationary";

            // Oppretter en liste
            List<ArtificialPlayer> players = new List<ArtificialPlayer>();

            // Oppretter kommando
            SqlCommand cmd = new SqlCommand();

            // Setter innstillinger
            cmd.CommandText = GetAllArtificialPlayersQuery;
            cmd.Parameters.Add(new SqlParameter("IsStationary", false));
            cmd.Connection = connection;

            try
            {
                // Kobler til databasen
                connection.Open();

                // Utfører kommando
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Legger til kunstig spiller i liste
                        players.Add(new ArtificialPlayer { ID = reader.GetInt32(0), LocationID = reader.GetInt32(6) });
                    }
                    reader.Close();
                }

                // Lukker tilkobling til databasen
                connection.Close();

                Log("Found " + players.Count + " artificial players in database.");
            }
            catch (Exception e)
            {
                Log(e.Message);
            }

            return players;
        }

        /// <summary>
        /// Metoden henter ut alle åpne lokasjoner (IsLocked = false) knyttet til gitt lokasjon.
        /// Lokasjoner har mange til mange relasjoner i databasen.
        /// </summary>
        public List<int> GetConnectedLocations(int locationID)
        {
            // SQL setning 
            string GetConnectedLocationsQuery = "SELECT * FROM LocationConnections WHERE LocationOne_LocationID = @LocationID OR LocationTwo_LocationID = @LocationID";

            // Oppretter liste
            List<int> locations = new List<int>();

            // Oppretter kommando
            SqlCommand cmd = new SqlCommand();

            // Setter innstillinger
            cmd.CommandText = GetConnectedLocationsQuery;
            cmd.Parameters.Add(new SqlParameter("LocationID", locationID));
            cmd.Connection = connection;

            try
            {
                // Kobler til databasen
                connection.Open();

                // Utfører kommando
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Sjekker at forbindelsen ikke er låst
                        if (!reader.GetBoolean(1))
                        {
                            // Lagrer lokasjonen som den kunstige spilleren ikke befinner seg på.
                            if (reader.GetInt32(3) == locationID)
                                locations.Add(reader.GetInt32(4));
                            else
                                locations.Add(reader.GetInt32(3));
                        }
                    }
                    reader.Close();
                }

                // Lukker tilkobling til databasen
                connection.Close();

                Log("Location " + locationID + " is connected to " + locations.Count + " locations");
            }
            catch (Exception e)
            {
                Log(e.Message);
            }

            return locations;
        }

        /// <summary>
        /// Metoden oppdaterer en gitt kunstig spillers lokasjon.
        /// </summary>
        public void UpdateArtificialPlayerLocation(int playerID, int locationID)
        {
            // SQL setning
            string UpdateArtificialPlayerLocationQuery = "UPDATE ArtificialPlayers SET LocationID = @LocationID WHERE ArtificialPlayerID = @PlayerID";

            // Oppretter kommando
            SqlCommand cmd = new SqlCommand();

            // Setter innstillinger
            cmd.CommandText = UpdateArtificialPlayerLocationQuery;
            cmd.Parameters.Add(new SqlParameter("PlayerID", playerID));
            cmd.Parameters.Add(new SqlParameter("LocationID", locationID));
            cmd.Connection = connection;

            try
            {
                // Kobler til databasen
                connection.Open();

                // Oppdaterer lokasjonen til gitt artificial player
                int check = cmd.ExecuteNonQuery();

                if (check == 1)
                    Log("Player " + playerID + " moved to " + locationID);

                // Lukker tilkobling til databasen
                connection.Close();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
        }

        /// <summary>
        /// Metoden logger gitt tekst dersom Debug = true.
        /// </summary>
        private void Log(string text)
        {
            if (Debug)
            {
                Console.WriteLine(DateTime.Now + " " + text);
            }
        }
    }
}
