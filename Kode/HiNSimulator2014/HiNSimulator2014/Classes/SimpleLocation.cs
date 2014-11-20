using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiNSimulator2014.Classes
{
    /// <summary>
    /// Enkel klasse med relevant info som sendes fra server til Movement.cshtml.
    /// I stedet for å sende hele Location-klassen, brukes denne kompakte klassen
    /// for å (forhåpentligvis) få ting til å gå litt kjappere..
    /// Andreas Dyrøy Jansson
    /// </summary>
    public class SimpleLocation
    {
        public int LocationId { get; set; }
        public String LocationInfo { get; set; }
        public String LocationName { get; set;}
        public List<SimpleLocation> ConnectedLocations { get { return connectedLocations;} }
        public int ImageID { get; set; }
        public int keyReturn { get; set; }


        private List<SimpleLocation> connectedLocations = new List<SimpleLocation>();

        public SimpleLocation()
        {
            ImageID = -1; // Som default har ikke en lokasjon et bilde
        }

        public void AddLocation(SimpleLocation sl)
        {
            connectedLocations.Add(sl);
        }
    }
}