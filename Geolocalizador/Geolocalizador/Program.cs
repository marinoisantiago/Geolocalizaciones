using Geolocalizador.Entidades;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geolocalizador
{
    class Program
    {
        /**
         * 
         * El nombre de la cola y el HostName DEBERIAN salir de la configuración (appsettings).
         * 
         **/
        public static string colaPregunta = "colaPreguntaCoordenada";
        public static string colaRespuesta = "colaRespuestaCoordenada";
        public static string hostrabbitmq = "localhost";
        


        static void Main(string[] args)
        {

            Coordenada coordenada;
            Direccion direccion = new Direccion();


            String colaPreguntaCoordenada = colaPregunta;
            var factoryPregunta = new ConnectionFactory() { HostName = hostrabbitmq };

            using (var connectionPregunta = factoryPregunta.CreateConnection())
            {
                using (var channelPregunta = connectionPregunta.CreateModel())
                {

                    channelPregunta.QueueDeclare(colaPreguntaCoordenada, false, false, false, null);
                    var consumer = new EventingBasicConsumer(channelPregunta);
                         
                    consumer.Received += (model, ea) =>
                    {
                        #region Procesa mensaje de la cola                        
                        var body = ea.Body.ToArray();

                        string jsonDireccion = Encoding.Default.GetString(body);
                        direccion = JsonConvert.DeserializeObject<Direccion>(jsonDireccion);
                        #endregion

                        #region Armado URL
                        string numero = direccion.Numero.Trim();
                        string calle = direccion.Calle.Trim().Replace(" ", "+");
                        string codigoPostal = direccion.Codigo_postal.Trim().Replace(" ", "+");
                        string ciudad = direccion.Ciudad.Trim().Replace(" ", "+");
                        string provincia = direccion.Provincia.Trim().Replace(" ", "+");
                        string pais = direccion.Pais.Trim().Replace(" ", "+");

                        var url = @"https://nominatim.openstreetmap.org/search?q=" + numero + "+" + calle + "," + codigoPostal + "," + ciudad + "," + provincia + "," + pais + "&format=json&polygon=1&addressdetails=1";
                        #endregion

                        #region Consulta a OSM                        
                        var client = new RestClient(url);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);

                        string jsonRespuestaOSM = response.Content;
                        var respuestaOSM = JsonConvert.DeserializeObject<List<RespuestaqOSM>>(jsonRespuestaOSM);
                        #endregion

                        coordenada = new Coordenada(direccion.Id, respuestaOSM[0].lat, respuestaOSM[0].lon);

                        #region Puesta en cola de respuesta coordenada
                        var factoryRespuesta = new ConnectionFactory() { HostName = hostrabbitmq };
                        using (var connectionRespuesta = factoryRespuesta.CreateConnection())
                        {
                            using (var channelRespuesta = connectionRespuesta.CreateModel())
                            {
                                string colaRespuestaCoordenada = colaRespuesta;
                                channelRespuesta.QueueDeclare(colaRespuestaCoordenada, false, false, false, null);


                                var jsonRespuesta = JsonConvert.SerializeObject(coordenada, Formatting.Indented);
                                var bodyRespuesta = Encoding.UTF8.GetBytes(jsonRespuesta);

                                channelRespuesta.BasicPublish("", colaRespuestaCoordenada, null, bodyRespuesta);
                            }
                        }
                        #endregion
                    };

                    channelPregunta.BasicConsume(queue: colaPreguntaCoordenada,
                                    autoAck: true,
                                    consumer: consumer);

                    Console.WriteLine(" Tocar [enter] para salir.");
                    Console.ReadLine();
                }
            }
 
           
        }
    }
}
