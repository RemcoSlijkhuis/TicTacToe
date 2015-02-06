using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace TicTacToeServer
{
    /// <summary>
    /// Provides in memory games
    /// </summary>
    internal static class GameStateHelper
    {
        public static long TotalRequestsServed
        {
            get { return totalRequestsServed; }
        }
        private static long totalRequestsServed;

        public static DateTime UpSince { get; private set; }

        public static ConcurrentDictionary<Guid, ServerGameState> Games { get; private set; }

        public static Type ServerEngineType { get; private set; }

        public static void Reset()
        {
            Games.Clear();   
        }

        public static void RegisterNewPlayer(Guid playerId, string name)
        {
            Logger.Current.Info("New player online: {0}. He's taking on the boss :)", name);
                
            Games.TryAdd(playerId, new ServerGameState { Name = name });
        }

        public static Guid GetPlayerId(string name)
        {
            return Games.Where(x => x.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Select(x => x.Key).FirstOrDefault();
        }

        public static void AddRequest()
        {
            Interlocked.Increment(ref totalRequestsServed);
        }

        public static string GetStatsHtml(bool addRefreshTag = true)
        {
            var stats = Games.Select(x => new
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
                       .AppendLine("<title>TicTacToe Statistics</title>");

            if (addRefreshTag)
                htmlBuilder.AppendLine("<meta http-equiv=\"refresh\" content=\"5\" />");

            htmlBuilder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />")
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

            var winningPlayer = "The Boss";

            var owningPlayer = stats.Where(x => x.PercentageWon >= 50).OrderByDescending(x => x.PercentageWon).FirstOrDefault();
            if (owningPlayer != null)
                winningPlayer = owningPlayer.PlayerName;

            if (!stats.Any())
                htmlBuilder.AppendLine("<h2>No games have been played yet...</h2>");
            else
            {
                var upSince = GameStateHelper.UpSince;
                var totalRequests = GameStateHelper.TotalRequestsServed;
                var totalSecondsOnline = Math.Max(1, (DateTime.Now - upSince).TotalSeconds);

                htmlBuilder.AppendLine(string.Format("<h2>Current leader: {0}</h2>", winningPlayer))
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

            return htmlBuilder.ToString();
        }

        static GameStateHelper()
        {
            Games = new ConcurrentDictionary<Guid, ServerGameState>();
            ServerEngineType = Type.GetType(ConfigurationManager.AppSettings["ServerImplementation"]);
            totalRequestsServed = 0;
            UpSince = DateTime.Now;
        }
    }
}
