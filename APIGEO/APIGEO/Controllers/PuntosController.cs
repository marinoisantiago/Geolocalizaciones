using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using APIGEO.Datos;
using APIGEO.Entidades;
using APIGEO.Modelo;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace APIGEO.Controllers
{

    [ApiController]
    [Route("geolocalizar")]
    public class PuntosController : ControllerBase
    {
        private readonly APIGEOContext _aPIGEOContext;

        /**
          * 
          * El nombre de la cola y el HostName DEBERIAN salir de la configuración (appsettings).
          * 
          **/
        public static string colaPregunta = "colaPreguntaCoordenada";        
        public static string hostrabbitmq = "localhost";


        public PuntosController(APIGEOContext contexto)
        {
            _aPIGEOContext = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Punto>>> GetPuntos()
        {
            return await _aPIGEOContext.Puntos.ToListAsync();
        }

        /// <summary>
        /// Devuelve las coordenadas del Id (punto) recibido.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>      
        [HttpGet("{id}")]        
        public async Task<ActionResult<Punto>> GetPunto(int id)
        {
            var punto = await _aPIGEOContext.Puntos.FindAsync(id);

            if (punto == null)
            {
                return NotFound();
            }

            return Ok(new RespuestaCoordenada(punto.Id, punto.Latitud, punto.Longitud, (punto.Geolocalizado ? "TERMINADO" : "PROCESANDO")));
        }

        /// <summary>
        /// Agrega un nuevo punto.
        /// </summary>
        /// <param name="punto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Punto>> NuevaDireccion(Punto punto)
        {
            _aPIGEOContext.Puntos.Add(punto);
            await _aPIGEOContext.SaveChangesAsync();

            Direccion direccion = new Direccion(punto.Id, punto.Calle, punto.Numero, punto.Ciudad, punto.Codigo_postal, punto.Provincia, punto.Pais);

            var factoryPregunta = new ConnectionFactory() { HostName = hostrabbitmq };
            using (var connectionRespuesta = factoryPregunta.CreateConnection())
            {
                using (var channelRespuesta = connectionRespuesta.CreateModel())
                {
                    string colaPreguntaCoordenada = colaPregunta;
                    channelRespuesta.QueueDeclare(colaPreguntaCoordenada, false, false, false, null);


                    var jsonPregunta = JsonConvert.SerializeObject(direccion, Formatting.Indented);
                    var bodyPregunta = Encoding.UTF8.GetBytes(jsonPregunta);

                    channelRespuesta.BasicPublish("", colaPreguntaCoordenada, null, bodyPregunta);
                }
            }


            return Accepted(new RespuestaNuevoPunto(punto.Id));            
        }

        
        /// <summary>
        /// Actualiza un punto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="puntoCoordenadas"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> GeolocalizarPunto(int id, JsonPatchDocument<Punto> puntoCoordenadas)
        {
            var punto = await _aPIGEOContext.Puntos.FindAsync(id);

            if (punto == null)
            {
                return NotFound();
            }

            punto.Geolocalizado = true;
            puntoCoordenadas.ApplyTo(punto, ModelState);

            _aPIGEOContext.Update(punto);
            await _aPIGEOContext.SaveChangesAsync();

            return Ok(punto);
                
        }
        
    }
}
