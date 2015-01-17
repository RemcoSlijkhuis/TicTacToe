using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TicTacToeSampleHttpClient.Extensions;

namespace TicTacToeSampleHttpClient.Engines
{
    public class GamePlayerFirstChoice
    {
        private readonly string apiUrl;

        public GamePlayerFirstChoice(string apiUrl, string name)
        {
            this.apiUrl = apiUrl;
            Name = name;
        }

        public string Name { get; set; }

        public Task StartPlaying(CancellationToken token)
        {
            Guid playerId;
            using (var httpClient = new WebClient())
            {
                var playerString = httpClient.DownloadString(apiUrl + "register?name=" + HttpUtility.UrlEncode(Name));
                playerId = Guid.Parse(playerString);
            }

            return Task.Run(() => PlayTheGame(playerId, token), token);
        }

        private void PlayTheGame(Guid playerId, CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                    return;

                using (var httpClient = new WebClient())
                {
                    var game = httpClient.DownloadString(apiUrl + "startGame?playerId=" + HttpUtility.UrlEncode(playerId.ToString()));

                    while (game != "")
                    {
                        if (token.IsCancellationRequested)
                            return;

                        var availableMovesString = httpClient.DownloadString(apiUrl + "getAvailableMoves?playerId=" + HttpUtility.UrlEncode(playerId.ToString())).ToCharArray();
                        var availableMoves = new List<Tuple<int, int, int, int>>();
                        var availableMovesSlices = availableMovesString.ToChunks(9).ToList();

                        for (var sbcolumn = 0; sbcolumn < 3; sbcolumn++)
                        {
                            for (var sbrow = 0; sbrow < 3; sbrow++)
                            {
                                var board = availableMovesSlices[sbcolumn * 3 + sbrow].ToList();

                                if (board.All(x => x != 'Y'))
                                    continue;

                                for (var x = 0; x < 3; x++)
                                {
                                    for (var y = 0; y < 3; y++)
                                    {
                                        var move = board[x * 3 + y];

                                        if (move == 'Y')
                                        {
                                            availableMoves.Add(new Tuple<int, int, int, int>(sbcolumn, sbrow, x, y));
                                        }
                                    }
                                }
                            }
                        }

                        // Your implemention here
                        var currentMove = availableMoves[0];

                        game = httpClient.DownloadString(apiUrl + string.Format("makeMove?boardColumn={0}&boardRow={1}&cellColumn={2}&cellRow={3}&playerId={4}",
                            currentMove.Item1, currentMove.Item2, currentMove.Item3, currentMove.Item4, playerId.ToString()));

                        // If the length of game is 81, it only contains the game state.
                        // If the length of game is 82, it contains the game state followed by the 'winner'.
                        if (game.Length == 82)
                        {
                            game = game.Substring(81, 1);
                        }

                        if (game.Equals("2", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine(Name + " has won a game! Yeah!");
                            game = "";
                        }
                        else if (game.Equals("1", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine(Name + " lost a game! ahhhhh!");
                            game = "";
                        }
                        else if (game.Equals("3", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine(Name + " has drawed a game! oh well!");
                            game = "";
                        }
                    }
                }
            }
        }
    }
}