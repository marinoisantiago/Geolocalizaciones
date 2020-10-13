using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIGEO.Modelo
{
    public class Punto
    {
        public int Id { get; set; }
        [Required]
        public string Calle { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Ciudad { get; set; }
        [Required]
        public string Codigo_postal { get; set; }
        [Required]
        public string Provincia { get; set; }
        [Required]
        public string Pais { get; set; }

        [Column(TypeName = "decimal(18,13)")] 
        public decimal Latitud { get; set; }

        [Column(TypeName = "decimal(18,13)")]
        public decimal Longitud { get; set; }

        public bool Geolocalizado { get; set; }
    }
}
