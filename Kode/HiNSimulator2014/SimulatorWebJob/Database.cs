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
    /// Metodene benyttes for å simulere bevegelse av artificial player
    /// og at artificial player plukker opp / legger fra seg ting.
    /// </summary>
    class Database
    {
        // Tilkobling til databasen
        private SqlConnection connection;

        /// <summary>
        /// Konstruktør setter opp tilkobling til databasen
        /// </summary>
        public Database()
        {
            string connectionString = "Server=tcp:ao2fjuj8m9.database.windows.net,1433;Database=gruppe2db;User ID=gruppe2manageradmin@ao2fjuj8m9;Password=appelsinFarge5;Trusted_Connection=False;Encrypt=True;Connection Timeout=30";
            connection = new SqlConnection(connectionString);

            Console.Out.WriteLine("Database connection created");
        }

        /// <summary>
        /// Metoden henter alle kunstige spillere (som kan bevege seg, IsStationary = false) fra databasen.
        /// </summary>
        public List<ArtificialPlayer> GetAllArtificialPlayers()
        {
            // SQL setning
            string GetAllArtificialPlayersQuery = "SELECT ArtificialPlayerID, Name, LocationID " +
                "FROM ArtificialPlayers WHERE IsStationary = @IsStationary";

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
                        players.Add(new ArtificialPlayer { ID = reader.GetInt32(0), 
                            Name = reader.GetValue(1).ToString(), LocationID = reader.GetInt32(2) });
                    }
                    reader.Close();
                }

                // Lukker tilkobling til databasen
                connection.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
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
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
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

                // Lukker tilkobling til databasen
                connection.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Metoden henter de tingene på gitt lokasjon som en artificial player har
        /// mulighet til å plukke opp (IsStationary = false, ArtificialPlayerID = null,
        /// CurrentOwner_Id = null). Alstå ting om ikke sitter fast eller befinner seg i
        /// en ekte spillers/kunstig aktørs inventory.
        /// </summary>
        public List<Thing> GetAllThingsInLocation(int locationID)
        {
            // SQL setning
            string GetAllThingsInLocationQuery = "SELECT ThingID, Name FROM Things " +
                "WHERE LocationID = @LocationID AND IsStationary = @IsStationary " +
                "AND ArtificialPlayerID IS NULL AND CurrentOwner_Id IS NULL";

            // Oppretter en liste
            List<Thing> things = new List<Thing>();

            // Oppretter kommando
            SqlCommand cmd = new SqlCommand();

            // Setter innstillinger
            cmd.CommandText = GetAllThingsInLocationQuery;
            cmd.Parameters.Add(new SqlParameter("LocationID", locationID));
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
                        // Legger til ting i liste
                        things.Add(new Thing{ ID = reader.GetInt32(0), Name = reader.GetValue(1).ToString() });
                    }
                    reader.Close();
                }

                // Lukker tilkobling til databasen
                connection.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return things;
        }

        /// <summary>
        /// Metoden henter de tingene en gitt artificial player holder.
        /// </summary>
        public List<Thing> GetThingsHeldByArtificialPlayer(int playerID)
        {
            // SQL setning
            string GetThingsHeldByArtificialPlayerQuery = "SELECT ThingID, Name FROM Things WHERE ArtificialPlayerID = @ArtificialPlayerID";

            // Oppretter en liste
            List<Thing> things = new List<Thing>();

            // Oppretter kommando
            SqlCommand cmd = new SqlCommand();

            // Setter innstillinger
            cmd.CommandText = GetThingsHeldByArtificialPlayerQuery;
            cmd.Parameters.Add(new SqlParameter("ArtificialPlayerID", playerID));
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
                        // Legger til ting i liste
                        things.Add(new Thing { ID = reader.GetInt32(0), Name = reader.GetValue(1).ToString() });
                    }
                    reader.Close();
                }

                // Lukker tilkobling til databasen
                connection.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return things;
        }

        /// <summary>
        /// Metoden setter en tings lokasjon til en gitt lokasjon.
        /// Benyttes når artificial player legger ned en ting.
        /// </summary>
        public void UpdateThingLocationToLocation(int locationID, int thingID)
        {
            // SQL setning
            string UpdateThingLocationToLocationQuery = "UPDATE Things SET LocationID = @LocationID,  " +
                "ArtificialPlayerID = NULL WHERE ThingID = @ThingID";

            // Oppretter kommando
            SqlCommand cmd = new SqlCommand();

            // Setter innstillinger
            cmd.CommandText = UpdateThingLocationToLocationQuery;
            cmd.Parameters.Add(new SqlParameter("LocationID", locationID));
            cmd.Parameters.Add(new SqlParameter("ThingID", thingID));
            cmd.Connection = connection;

            try
            {
                // Kobler til databasen
                connection.Open();

                // Oppdaterer lokasjonen til gitt ting
                int check = cmd.ExecuteNonQuery();
                
                // Lukker tilkobling til databasen
                connection.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Metoden setter en tings lokasjon til en gitt kunstig spiller.
        /// Benyttes når artificial player plukker opp en ting
        /// </summary>
        public void UpdateThingLocationToArtificialPlayer(int playerID, int thingID)
        {
            // SQL setning
            string UpdateThingLocationToArtificialPlayerQuery = "UPDATE Things SET ArtificialPlayerID = @ArtificialPlayerId, " +
                "LocationID = NULL WHERE ThingID = @ThingID";

            // Oppretter kommando
            SqlCommand cmd = new SqlCommand();

            // Setter innstillinger
            cmd.CommandText = UpdateThingLocationToArtificialPlayerQuery;
            cmd.Parameters.Add(new SqlParameter("ArtificialPlayerId", playerID));
            cmd.Parameters.Add(new SqlParameter("ThingID", thingID));
            cmd.Connection = connection;

            try
            {
                // Kobler til databasen
                connection.Open();

                // Oppdaterer lokasjonen til gitt artificial player
                int check = cmd.ExecuteNonQuery();

                // Lukker tilkobling til databasen
                connection.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}
