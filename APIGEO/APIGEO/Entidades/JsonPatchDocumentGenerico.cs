using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGEO.Entidades
{
    public class JsonPatchDocumentGenerico
    {

        public JsonPatchDocumentGenerico(decimal valor, string campo, string accion)
        {
            value = valor;
            path = campo;
            op = accion;
        }
        public decimal value { get; set; }
        public string path { get; set; }
        public string op { get; set; }
        

    }
}
