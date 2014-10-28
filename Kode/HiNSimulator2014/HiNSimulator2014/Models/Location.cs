using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen representerer en lokasjon, det være seg trapp, gang eller lignende.
    /// </summary>
    public class Location
    {
        public int LocationID { get; set; }
        public String LocationType { get; set; }//Om det er en gang, rom, trapp osv.
        public String AcessTypeRole { get; set; }//What is this? Pls
        public String ShortDescription { get; set; }//Kort beskrivelse av stedet.
        public String LongDescription { get; set; }//Detaljert beskrivelse av stedet.
        public int? ImageID { get; set; }//Fremmednøkkel til bilde
        public virtual Image Images { get; set; }//Tilhørende bilde.
    }
}