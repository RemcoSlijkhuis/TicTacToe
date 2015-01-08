using System;
using System.Linq;
using TicTacToeSampleHttpClient.Extensions;

namespace TicTacToeSampleHttpClient.Model
{
    public class Game
    {
        public SmallBoard[,] SmallBoards { get; set; }

        public Game()
        {
            SmallBoards = new SmallBoard[3, 3];

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    SmallBoards[x, y] = new SmallBoard(x, y);
                    SmallBoards[x, y].EmptyCells();
                }
            }
        }

        public static Game FromState(string gameState)
        {
            var game = new Game();
            var boards = gameState.ToCharArray().ToChunks(9).ToList();

            if (boards.Count != 9)
                throw new InvalidOperationException("Invalid gamestate");

            for (var sbcolumn = 0; sbcolumn < 3; sbcolumn++)
            {
                for (var sbrow = 0; sbrow < 3; sbrow++)
                {
                    var boardState = boards[sbcolumn * 3 + sbrow];

                    var board = game.SmallBoards[sbcolumn, sbrow];
                    var boardMoves = boardState.Select(x => (x - '0')).ToList();

                    for (var x = 0; x < 3; x++)
                    {
                        for (var y = 0; y < 3; y++)
                        {
                            var playerMove = boardMoves[x * 3 + y];

                            if (playerMove <= 0) continue;
                            if (playerMove < 0 || playerMove > 2)
                                throw new InvalidOperationException("Invalid gamestate");

                            board.SetOwner(x, y, playerMove);
                        }
                    }
                }
            }

            return game;
        }
    }
}