using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen angir hvilke kommandoer som er gyldige for en spesifikk kunstig aktør
    /// </summary>
    public class ValidCommandsForArtificialPlayers
    {
        public virtual ArtificialPlayer ArtificialPlayer { get; set; }
        public virtual Commands Command { get; set; }
    }
}