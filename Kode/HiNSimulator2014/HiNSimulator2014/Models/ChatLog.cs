using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen lagrer historikk over meldinger som er sendt av spillere
    /// </summary>
    public class ChatLog
    {
        public int MessageID { get; set; }

        public virtual ApplicationUser FromPlayer { get; set; } // Spilleren som sendte meldingen

        public int LocationID { get; set; }
        public virtual Location ToLocation { get; set; } // Hvilket spilleren er i når den sendes (bare andre spillere i dette rommet mottar meldinga)
        
        public DateTime Timestamp { get; set; } // Når meldinga ble sendt
        
        public String MessageText { get; set; } // Selve meldinga
    }
}