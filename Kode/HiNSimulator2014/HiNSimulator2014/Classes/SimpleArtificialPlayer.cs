using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Classes
{
    public class SimpleArtificialPlayer
    {
        public int ArtificialPlayerID { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String Description { get; set; }
        public int? ImageID { get; set; }

        public SimpleArtificialPlayer()
        {
        }

    }
}