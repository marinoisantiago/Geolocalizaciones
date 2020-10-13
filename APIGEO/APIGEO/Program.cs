using System;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text;
using APIGEO.Entidades;
using System.Net;
using System.IO;


namespace APIGEO
{
    public class Program
    {           
        /**
             * 
             * El nombre de la cola, el HostName y la url del sitio DEBERIAN salir de la configuración (appsettings).
             * 
        **/
        public static string colaRespuesta = "colaRespuestaCoordenada";
        public static string hostrabbitmq = "localhost";
        public static string urlSitio = @"https://localhost:44332/geolocalizar/";

        public static void Main(string[] args)
        {

            
            Coordenada coordenada;
            /**
             * 
             * El nombre de la cola y el HostName DEBERIA salir de la configuración (appsettings).
             * 
             * **/
            String cola = colaRespuesta;          
            var factory = new ConnectionFactory() { HostName = hostrabbitmq };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(cola, false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        #region Procesa mensaje de la cola                        
                        var body = ea.Body.ToArray();

                        string json = Encoding.Default.GetString(body);

                        coordenada = JsonConvert.DeserializeObject<Coordenada>(json);

                        var jsonPatch = "[" + JsonConvert.SerializeObject(new JsonPatchDocumentGenerico(coordenada.Latitud, @"/Latitud", "replace"), Formatting.Indented) + "," + 
                        JsonConvert.SerializeObject(new JsonPatchDocumentGenerico(coordenada.Longitud, @"/Longitud", "replace"), Formatting.Indented) + "]";
                        #endregion

                        #region Actualizacion Coordenadas                        
                        //La ruta podría salir de la configuración en appsettings.
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlSitio + coordenada.Id.ToString());
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "PATCH";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) 
                        {
                            streamWriter.Write(jsonPatch);
                        }
                        using (WebResponse webResponse = httpWebRequest.GetResponse())
                        {
                            //HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
                        }
                        #endregion

                    };
                    channel.BasicConsume(queue: cola,
                                         autoAck: true,
                                         consumer: consumer);
                    
                    CreateHostBuilder(args).Build().Run();
                }
            }         

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
