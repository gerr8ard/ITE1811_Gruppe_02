using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen lagrer listen over alle kommandoer i spillet
    /// </summary>
    public class Command
    {
        [Key]
        public int CommandID { get; set; }

        public String Name { get; set; } // Navnet på kommandoen
        public String Description { get; set; } // Beskrivelse av hva kommandoen gjør
    }
}