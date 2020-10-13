using System;
using System.Collections.Generic;
using System.Text;

namespace Geolocalizador.Entidades
{
   
    public class RespuestaqOSM
    {
        public int place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public int osm_id { get; set; }
        public string[] boundingbox { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
        public string display_name { get; set; }
        public string _class { get; set; }
        public string type { get; set; }
        public float importance { get; set; }
        public Address address { get; set; }
    }

    public class Address
    {
        public string house_number { get; set; }
        public string road { get; set; }
        public string suburb { get; set; }
        public string city_district { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
    }

    
}
