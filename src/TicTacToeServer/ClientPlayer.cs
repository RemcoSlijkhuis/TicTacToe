using System.Collections.Generic;
using TicTacToeShared;

namespace TicTacToeServer
{
    /// <summary>
    /// This player can be controlled from the outside, in order for the HTTP calls to control the next move
    /// </summary>
    internal class ClientPlayer : IPlayerEngine
    {
        private Move nextMove;

        public void PrepareNextMove(Move move)
        {
            nextMove = move;
        }

        public void BeforeGame()
        {
        }

        public void AfterGame(int player, Board board, List<Move> moves)
        {
        }

        public Move GetMove(int player, Board board, List<Move> moves)
        {
            var returnMove = nextMove;
            nextMove = null;
            return returnMove;
        }
    }
}
