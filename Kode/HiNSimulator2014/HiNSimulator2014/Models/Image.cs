using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Models
{
    /// <summary>
    /// Skrevet av: Tina Ramsvik, Alexander Lindquister, Andreas Jansson og Pål Skogsrud
    /// 
    /// Denne klassen lagrer et bilde som kan vises sammen med et rom eller et objekt.
    /// </summary>
    public class Image
    {
        public int ImageID { get; set; }
        public String ImageText { get; set; }
        public byte[] ImageBlob { get; set; }
    }
}