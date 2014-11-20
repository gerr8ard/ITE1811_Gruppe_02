using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Classes
{
    public class SimpleThing
    {
        public int ThingID { get; set; }
        public int? ImageID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int? KeyLevel { get; set; }
        public bool PlayerWritable { get; set; }
        public String WrittenText { get; set; }

        public SimpleThing()
        {

        }
    }
}