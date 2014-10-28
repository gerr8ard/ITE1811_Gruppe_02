using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen angir hva en response til en kuntig aktør om en spiller prøver å kommunisere med den
    /// </summary>
    public class ArtificialPlayerResponse
    {
        public int ArtificialPlayerResponseID { get; set; }
        public ArtificialPlayer ArtificialPlayer { get; set; }
        public String ResponseText { get; set; }
    }
}