using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Classes
{
    /// <summary>
    /// Klasse som brukes av Chat-relaterte operasjoner
    /// i stedet for å bruke hele ApplicationUser-klassen
    /// Skrevet av: Pål Gerrard Gaare-Skogsrud
    /// </summary>
    public class SimpleUser
    {
        public string PlayerName { get; set; }
        public string PlayerId { get; set; }
        public string LocationId { get; set; }
        public string ConnectionId { get; set; }

        public SimpleUser()
        {

        }


    }
}