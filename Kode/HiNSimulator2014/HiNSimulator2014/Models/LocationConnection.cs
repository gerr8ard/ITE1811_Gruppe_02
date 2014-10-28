using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
/// 
/// Denne klassen representerer en dør, trapp eller en annen forbindelse mellom to rom.
/// </summary>
namespace HiNSimulator2014.Models
{
    public class LocationConnection
    {
        public int LocationConnectionID { get; set; }
        public bool IsLocked { get; set; }//Angir om forbindelsen stengt
        public int RequiredKeyLevel { get; set; }//Angir hvilken rettighet man må ha for å få tilgang.

        public int LocationIDOne { get; set; }
        public virtual Location LocationOne { get; set; }//Skiller rommene fra hverandre

        public int LocationIDTwo { get; set; }
        public virtual Location LocationTwo { get; set; }//Skiller rommene fra hverandre
    }
}