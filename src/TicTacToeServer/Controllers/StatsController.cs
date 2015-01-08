using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TicTacToeServer.Controllers
{
    /// <summary>
    /// Serves as a global stats controller
    /// </summary>
    [RoutePrefix("stats")]
    public class StatsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            var stats = GameStateHelper.Games.Select(x => new
            {
                PlayerId = x.Key,
                PlayerName = x.Value.Name,
                GamesPlayed = x.Value.Stats.NumberOfGames,
                ServerWins = x.Value.Stats.Player1Wins,
                PlayerWins = x.Value.Stats.Player2Wins,
                Draws = x.Value.Stats.Draws,
                PercentageWon = (double)(((double)x.Value.Stats.Player2Wins / (double)Math.Max(1, (double)x.Value.Stats.NumberOfGames - (double)x.Value.Stats.Draws)) * 100)
            }).OrderByDescending(x => x.PercentageWon).ToList();

            var htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine("<!DOCTYPE html>")
                       .AppendLine("<html>")
                       .AppendLine("<head>")
                       .AppendLine("<title>TicTacToe Statistics</title>")
                       .AppendLine("<meta http-equiv=\"refresh\" content=\"5\" />")
                       .AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />")
                       .AppendLine("<meta http-equiv=\"Cache-Control\" content=\"NO-CACHE\" />")
                       .AppendLine("<meta http-equiv=\"PRAGMA\" content=\"NO-CACHE\" />")
                       .AppendLine("<style type=\"text/css\"> \n" +
                                        ".tftable {font-size:12px;color:#333333;width:100%;border-width: 1px;border-color: #a9a9a9;border-collapse: collapse;} \n" + 
                                        ".tftable th {font-size:12px;background-color:#b8b8b8;border-width: 1px;padding: 8px;border-style: solid;border-color: #a9a9a9;text-align:left;} \n" + 
                                        ".tftable tr {background-color:#cdcdcd;} \n" +
                                        ".tftable td {font-size:12px;border-width: 1px;padding: 8px;border-style: solid;border-color: #a9a9a9;} \n" +
                                   "</style>")
                       .AppendLine("</head>")
                       .AppendLine("<body>")
                       .AppendLine("<h1>TicTacToe Statistics</h1>");

            if (!stats.Any())
                htmlBuilder.AppendLine("<h2>No games have been played yet...</h2>");
            else
            {
                var upSince = GameStateHelper.UpSince;
                var totalRequests = GameStateHelper.TotalRequestsServed;
                var totalSecondsOnline = Math.Max(1, (DateTime.Now - upSince).TotalSeconds);

                htmlBuilder.AppendLine(string.Format("<h2>Current leader: {0}</h2>", stats.First().PlayerName))
                           .AppendLine(string.Format("<h3>Total seconds online {0}</h3>", totalSecondsOnline))
                           .AppendLine(string.Format("<h3>{0} requests since {1}, that's {2} requests/sec</h3>", totalRequests, upSince.ToShortTimeString(), totalRequests / totalSecondsOnline))
                           .AppendLine("<br />")
                           .AppendLine("<table class=\"tftable\" border=\"1\">")
                           .AppendLine("<tr><th>Player</th><th>Games played</th><th>Boss wins</th><th>Player wins</th><th>Draws</th><th>Percentage</th></tr>");

                foreach (var statsEntry in stats)
                {
                    htmlBuilder.AppendLine(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                                                         statsEntry.PlayerName,
                                                         statsEntry.GamesPlayed,
                                                         statsEntry.ServerWins,
                                                         statsEntry.PlayerWins,
                                                         statsEntry.Draws,
                                                         Math.Round(statsEntry.PercentageWon, 2, MidpointRounding.AwayFromZero)));
                }

                htmlBuilder.AppendLine("</table>");
            }

            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            return new HttpResponseMessage
            {
                Content = new StringContent(htmlBuilder.ToString(), Encoding.UTF8, "text/html")
            };
        }
    }
}