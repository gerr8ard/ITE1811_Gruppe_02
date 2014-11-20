using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen representerer en NPC / datastyrt spiller
    /// </summary>
    public class ArtificialPlayer
    {
        public int ArtificialPlayerID { get; set; }

        public String Name { get; set; } // Navnet som vises i spillet
        public String Type { get; set; } // Tittel/type (lærer, vaktmester osv..)
        public String Description { get; set; } // En beskrivelse av personen
        public String AccessLevel { get; set; } //Tilgangsrettigheter til personen
        public bool IsStationary { get; set; } // Om personen er "låst" til ett rom

        public int LocationID { get; set; } // Fremmednøkkel til nåværende posisjon
        public virtual Location CurrentLocation { get; set; } // Nåværende posisjon

        public int? ImageID { get; set; }
        public Image ImageObject { get; set; }
    }
}