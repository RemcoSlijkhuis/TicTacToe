using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TicTacToeSampleHttpClient.Engines;

namespace TicTacToeSampleHttpClient
{
    class Program
    {
        private const string ApiUrl = "http://localhost:9091/game/";

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadLine();

            var player1 = new GamePlayerWinMoves(ApiUrl, "De Baron");
            var player2 = new GamePlayerRandom(ApiUrl, "Bassie");
            var player3 = new GamePlayerRandom(ApiUrl, "B100");
            var player4 = new GamePlayerFirstChoice(ApiUrl, "Vlugge Japie");
            var player5 = new GamePlayerWinMovesFirst(ApiUrl, "Argibald Chagrijn");

            var cancellationTokenSource = new CancellationTokenSource();
            var games = new List<Task>
            {
                player1.StartPlaying(cancellationTokenSource.Token),
                player2.StartPlaying(cancellationTokenSource.Token),
                player3.StartPlaying(cancellationTokenSource.Token),
                player4.StartPlaying(cancellationTokenSource.Token),
                player5.StartPlaying(cancellationTokenSource.Token)
            };

            Console.ReadLine();
            cancellationTokenSource.Cancel();
            Task.WaitAll(games.ToArray());
        }
    }
}