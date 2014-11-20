using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen representerer en ting som befinner seg i spillet, tilhørende en lokasjon eller en spiller
    /// </summary>
    public class Thing
    {
        public int ThingID { get; set; }
        
        public int? ImageID { get; set; } // Fremmednøkkel til bilde
        public virtual Image ImageObject { get; set; } // Bilde av tingen
        
        public int? LocationID { get; set; } // Fremmednøkkel til lokasjon
        public virtual Location CurrentLocation { get; set; } // Stedet tingen befinner seg på om (null om det er en spiller som har tingen)
        
        public virtual ApplicationUser CurrentOwner { get; set; } // Spilleren som eier objektet (null om tingen befinner seg på en lokasjon)

        public int? CurrentArtificialPlayerID { get; set; } // Fremmednøkkel til CurrentArtificialPlayerOwner
        public ArtificialPlayer CurrentArtificialPlayerOwner { get; set; } // Kunstige aktøren som eier tingen

        public String Name { get; set; } // Navn på tingen
        public String Description { get; set; } // Beskrivelse av tingen

        public bool IsStationary { get; set; } // Angir om tingen er fast inventar i et rom eller om spilleren kan ta den med seg

        public int? KeyLevel { get; set; } // Om tingen er en nøkkel, angir dette hvilke dører den kan åpne

        public bool PlayerWritable { get; set; } // Angir om spillerene kan skrive på tingen
        public String WrittenText { get; set; } // Tekst som er skrevet på tingen (kan endres på av spillerne

    }
}