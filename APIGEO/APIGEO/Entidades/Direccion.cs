using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGEO.Entidades
{
    public class Direccion
    {
        public Direccion(int id, string calle, string numero, string ciudad, string codigo_posta, string provincia, string pais)
        {
            Id = id;
            Calle = calle;
            Numero = numero;
            Ciudad = ciudad;
            Codigo_postal = codigo_posta;
            Provincia = provincia;
            Pais = pais;
        }

        public int Id { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Ciudad { get; set; }

        public string Codigo_postal { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }
    }
}
