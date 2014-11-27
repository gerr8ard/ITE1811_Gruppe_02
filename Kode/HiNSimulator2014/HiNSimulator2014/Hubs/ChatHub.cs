using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using HiNSimulator2014.Models;
using HiNSimulator2014.Classes;
using System.Threading.Tasks;

namespace HiNSimulator2014.Hubs
{
    /// <summary>
    /// ChatHub.cs - Klasse som holder rede på alle spillere med en aktiv tilkobling til spillet.
    /// Spillere kan kalle metoder for å sende en melding til alle spillere i samme rom, eller
    /// starte en privat chat med valgt spiller i rommet. Bruker Groups for å gruppere
    /// spillere i samme rom. Basert på SignalR chat-eksempel fra
    /// http://www.codeproject.com/Articles/562023/Asp-Net-SignalR-Chat-Room
    /// Skrevet av Pål Gerrard Gaare-Skogsrud og Andreas Dyrøy Jansson
    /// </summary>
    public class ChatHub : Hub
    {
        // Repository
        private IRepository  repo = new Repository();
        // Liste med påloggede spillere
        static List<SimpleUser> onlinePlayers = new List<SimpleUser>();

        // Testkonstruktør
        public ChatHub()
        {
           
        }

        // Testkonstruktør
        public ChatHub(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Kalles når brukeren oppretter en forbindelse til hub'en
        /// </summary>
        /// <param name="userId">Brukers id (fra databasen)</param>
        /// <param name="locationId">Brukers nåværende posisjon</param>
        public void Connect(string userId, string locationId)
        {
            Debug.Write("\nmottar connect fra client ");

            // Henter ConnectionId for å identifisere klienten
            var connectionId = Context.ConnectionId;
            var user = repo.GetUserByID(userId);
            
            if (user != null)
            {
                var groupName = "loc_" + locationId;
                Debug.Write("\nKlient " + user.PlayerName + " kobler seg til gruppe " + groupName);

                // Legger klienten til i romgruppen
                Groups.Add(connectionId, groupName);

                // Sender beskjed til i de andre i gruppa om at en ny spiller kom inn i rommet
                Clients.OthersInGroup(groupName).addLocationPlayer(user.PlayerName, userId);
                
                // Sjekker om tilkoblet bruker allerede finnes i listen på hubben
                if (onlinePlayers.Count(x => x.ConnectionId == connectionId) == 0)
                {
                    // Legger han til, bruker SimpleUser-klassen
                    onlinePlayers.Add(new SimpleUser
                    {
                        ConnectionId = connectionId,
                        PlayerId = userId,
                        PlayerName = user.PlayerName,
                        LocationId = locationId.ToString()
                    });

                }
                else
                {
                    // Oppdaterer posisjon hvis han allerede finnes
                    var userL = onlinePlayers.Where(u => u.PlayerId.Equals(userId)).FirstOrDefault();
                    userL.LocationId = locationId;

                } 
                Debug.Write("\nNumber of connected clients " + onlinePlayers.Count + " gruppenavn " + groupName);
                // Sender listen med spillere i rommet tilbake til klienten som kalte funksjonen
                Clients.Caller.setPlayersInRoom(onlinePlayers);
            }
        }

        /// <summary>
        /// Kalles når en spiller går inn i et nytt rom
        /// </summary>
        /// <param name="locationId">Id'en til rommet spilleren går inn i</param>
        /// <param name="playerName">Navet på spilleren</param>
        /// <param name="userId">Id'en til spilleren</param>
        /// <returns>Beskjed om at en ny spiller kom inn i rommet</returns>
        public Task AddLocationPlayer(string locationId, string playerName, string userId)
        {
            var groupName = "loc_" + locationId;
            // Oppaterer locationId til bruker
            // http://stackoverflow.com/questions/19280986/best-way-to-update-an-element-in-a-generic-list
            var user = onlinePlayers.Where(u => u.PlayerId.Equals(userId)).FirstOrDefault();
            user.LocationId = locationId;
            // Sender listen med tilstedeværende brukere i rommet tilbake til klienten
            Clients.Caller.setPlayersInRoom(onlinePlayers);
            // Sender beskjed om at en ny spiller kom inn i rommet
            return Clients.OthersInGroup(groupName).addLocationPlayer(playerName, userId);
        }

        /// <summary>
        /// Sender melding til alle brukere i samme rom
        /// </summary>
        /// <param name="locationId">Rommet brukeren står i</param>
        /// <param name="name">Navnet på avsender</param>
        /// <param name="message">Meldingsteksten</param>
        public void Send(string locationId, string name, string message)
        {
            Debug.Write("mottar melding fra " + name + ": " + message);
            var groupName = "loc_" + locationId;
            // Sender melding til alle spiller i samme rom (gruppe)
            Clients.Group(groupName).addNewMessageToPage(name, message);
        }

        /// <summary>
        /// Sender en privat melding til valgt bruker
        /// </summary>
        /// <param name="toUserId">Mottakers brukerId</param>
        /// <param name="message">Meldingsteksten</param>
        public void SendPrivateMessage(string toUserId, string message)
        {
            // Finner avsenders ConnectionId
            string fromUserId = Context.ConnectionId;

            // Henter spillere fra listen over tilkoblede brukere
            var toUser = onlinePlayers.FirstOrDefault(x => x.PlayerId == toUserId);
            var fromUser = onlinePlayers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            // Sjekker at begge brukerene er tilkoblet/finnes i listen
            if(toUser != null && fromUser != null)
            {
                // Sender privat melding til spiller
                Clients.Client(toUser.ConnectionId).sendPrivateMessage(fromUser.PlayerId, fromUser.PlayerName, message);
            }
        }

        /// <summary>
        /// Kalles når en spiller går ut av rommet
        /// </summary>
        /// <param name="locationId">Id'en til rommet som forlates</param>
        /// <returns></returns>
        public Task leaveLocation(string locationId)
        {
            var groupName = "loc_" + locationId;
            // Fjerner spilleren fra gruppen
            return Groups.Remove(Context.ConnectionId, groupName);
        }



        /// <summary>
        /// Kalles når en bruker går ut av et rom
        /// </summary>
        /// <param name="playerName">Navnet på spilleren</param>
        /// <param name="locationId">Id'en til rommet spilleren gikk ut av</param>
        /// <param name="userId">Id'en til bruker</param>
        /// <returns>Beskjed til klientene i rommet om at spilleren gikk ut</returns>
        public Task RemoveLocationPlayer(string playerName, string locationId, string userId)
        {
            var groupName = "loc_" + locationId;

            if (locationId.Equals(""))
            {
                return null;
            }
            Debug.Write("\nmottar remove fra client " + playerName + " gruppenavn " + groupName);
            // Kaller de andre klienten i gruppen
            return Clients.OthersInGroup(groupName).removeLocationPlayer(userId, playerName);
        }

        /// <summary>
        /// Override OnDisconnected for å håndtere at en bruker logger seg av eller forlater
        /// siden, sender beskjed til de i rommet om at han "left"
        /// </summary>
        /// <param name="stopCalled">Klienten slutter å kalle hub'en</param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled = true)
        {
            var user = onlinePlayers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            if (user != null)
            {
                Debug.Write("\ninne i ondisconnected " + user.PlayerName);
                // Fjerner klienten fra onlinePlayers
                onlinePlayers.Remove(user);

                var groupName = "loc_" + user.LocationId;
                // Kaller de andre klientene i gruppen og gir beskjed om at en spiller har koblet seg av
                Clients.OthersInGroup(groupName).removeLocationPlayer(user.PlayerId, user.PlayerName);

            }

            return base.OnDisconnected(stopCalled);
        }

    }
}