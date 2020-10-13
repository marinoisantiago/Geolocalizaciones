using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGEO.Entidades
{
    public class RespuestaCoordenada : Coordenada
    {
        public RespuestaCoordenada(int id, decimal latitud, decimal longitud, string estado)
            : base(id, latitud, longitud)
        {
            Estado = estado;
        }         

        public string Estado { get; set; }
    }
}
