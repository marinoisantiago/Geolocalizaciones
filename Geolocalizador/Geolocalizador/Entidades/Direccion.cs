using System;
using System.Collections.Generic;
using System.Text;

namespace Geolocalizador.Entidades
{
    public class Direccion
    {
        public int Id { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Ciudad { get; set; }

        public string Codigo_postal { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }
    }
}
