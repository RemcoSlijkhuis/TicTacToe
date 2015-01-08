using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeShared;

namespace TicTacToeSampleEngines
{


    /// <summary>
    /// This player randomly selects an available move to play.
    /// </summary>
    public class RandomPlayer : IPlayerEngine
    {
        private static Random rnd = new Random();

        public void BeforeGame()
        {
            //nothing to initialize before a game
        }

        public void AfterGame(int player, Board board, List<Move> moves)
        {
            //nothing to analyse after a game
        }


        public Move GetMove(int player, Board board, List<Move> moves)
        {
            //get list of available moves
            var availMoves = board.GetAvailableMoves();

            int index = RandomPlayer.rnd.Next(availMoves.Count);
            return availMoves[index];

        }

    }

}
