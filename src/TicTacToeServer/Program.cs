using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin.Hosting;
using Owin;

namespace TicTacToeServer
{
    class Program
    {
        private static Timer statsTimer = new Timer(SaveStats, null, 1000, 30 * 1000);
        private static string currentFolder;
        private static string statsFolder;
        
        static void Main(string[] args)
        {
            Logger.Current.Info("Lucrasoft - TicTacToe server");

            currentFolder = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            statsFolder = Path.Combine(currentFolder, "Stats");

            if (!Directory.Exists(statsFolder))
                Directory.CreateDirectory(statsFolder);

            var serverPort = ConfigurationManager.AppSettings["ServerPort"];
            var url = "http://*:" + serverPort;

            var startOptions = new StartOptions { Port = Convert.ToInt32(serverPort) };
            startOptions.Urls.Add(url);

            using (StartService(startOptions))
            {
                Logger.Current.Info("Server now accepting requests on: {0}", url);
                Logger.Current.Info("Press q and enter to exit...");

                while (true)
                {
                    var consoleInput = Console.ReadLine();

                    if (HandleCommand(consoleInput))
                    {
                        break;
                    }
                }
            }
        }

        private static bool HandleCommand(string consoleInput)
        {
            switch (consoleInput.ToLower())
            {
                case "q":
                    return true;
                case "r":
                    GameStateHelper.Reset();
                    break;
                default:
                    Console.WriteLine("Did not understand command. Use q to quit, r to reset.");
                    break;
            }
            
            return false;
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

        private static void SaveStats(object state)
        {
            if (GameStateHelper.Games.Any())
            {
                var stats = GameStateHelper.GetStatsHtml(false);
                var statsFile = Path.Combine(statsFolder, "stats_" + DateTime.Now.Ticks + ".html");

                File.WriteAllText(statsFile, stats, Encoding.UTF8);    
            }
        }
    }
}
