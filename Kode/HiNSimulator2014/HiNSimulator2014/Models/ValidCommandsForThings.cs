using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{

    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen angir hvilke kommandoer som er gyldige for en spesifikk ting
    /// </summary>
    public class ValidCommandsForThings
    {
        public int ThingID { get; set; }
        public virtual Thing Thing { get; set; }

        public int CommandID { get; set; }
        public virtual Command Command { get; set; }
    }
}