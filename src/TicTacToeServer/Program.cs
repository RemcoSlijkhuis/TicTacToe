using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin.Hosting;
using Owin;

namespace TicTacToeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Current.Info("Lucrasoft - TicTacToe server");

            var serverPort = ConfigurationManager.AppSettings["ServerPort"];
            var url = "http://*:" + serverPort;

            var startOptions = new StartOptions { Port = Convert.ToInt32(serverPort) };
            startOptions.Urls.Add(url);

            using (StartService(startOptions))
            {
                Logger.Current.Info("Server now accepting requests on: {0}", url);
                Logger.Current.Info("Press q and enter to exit...");

                while (Console.ReadLine() != "q")
                {
                    Console.WriteLine("Did not understand command. Use q to quit.");
                }
            }
        }
        
        private static IDisposable StartService(StartOptions startOptions)
        {
            var server = WebApp.Start(startOptions, builder =>
            {
                var config = new HttpConfiguration();
                
                config.MapHttpAttributeRoutes();
                config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
                config.Services.Add(typeof(IExceptionLogger), new WebApiExceptionLogger());

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                    );
                
                builder.UseWebApi(config);
                
            });

            return server;
        }
    }
}
