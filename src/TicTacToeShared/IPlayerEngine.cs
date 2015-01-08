using System.Collections.Generic;

namespace TicTacToeShared
{
    /// <summary>
    /// Implement these methods to make a PlayerEngine
    /// </summary>
    public interface IPlayerEngine
    {
        void BeforeGame();
        void AfterGame(int player, Board board, List<Move> moves);
        Move GetMove(int player, Board board, List<Move> moves);
    }
}
