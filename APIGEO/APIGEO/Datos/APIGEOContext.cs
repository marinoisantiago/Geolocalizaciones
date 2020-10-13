using APIGEO.Modelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;


namespace APIGEO.Datos
{
    public class APIGEOContext : DbContext
    {
        public APIGEOContext(DbContextOptions<APIGEOContext> options):base(options)
        {
            
        }

        public DbSet<Punto> Puntos { get; set; }
    }
}
