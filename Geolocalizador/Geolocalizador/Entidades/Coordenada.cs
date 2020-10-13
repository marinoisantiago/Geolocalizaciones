using System;
using System.Collections.Generic;
using System.Text;

namespace Geolocalizador.Entidades
{
    public class Coordenada
    {

        public Coordenada(int id, decimal latitud, decimal longitud)
        {
            Id = id;
            Latitud = latitud;
            Longitud = longitud;
        }

        public int Id { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }

    }
}
