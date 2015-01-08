using System;
using System.Collections.Generic;
using TicTacToeShared;

namespace TicTacToeSampleEngines
{
    /// <summary>
    /// Sample engine.
    /// Selects a move based on a "scoring".
    /// </summary>
    public class MediumPointBasedPlayer : IPlayerEngine
    {

        private static Random rnd = new Random();

        public void BeforeGame() { }
        public void AfterGame(int player, Board state, List<Move> moves) { }


        public Move GetMove(int player, Board state, List<Move> moves)
        {
            //get list of available moves
            var availMoves = state.GetAvailableMoves();

            var maxPoints = -1;

            //hold the best move sofar, initialize with just the first available move.
            var bestMove = availMoves[0];

            //holds the second best move sofar, initialize with just the first available move.
            var secondBestMove = availMoves[0];

            //Loop through available moves and assign points to each move. 
            foreach (var move in availMoves)
            {
                var points = 0;

                if (Simulator.IsCorner(move)) { points += 1; }
                if (Simulator.IsCenter(move)) { points += 1; }
                if (Simulator.SmallBoardAlreadyWonByComponent(state, move, player)) { points += 1; }
                if (Simulator.DestinationBoardIsFull(state, move)) { points -= 2; }
                if (Simulator.IsWinningMove(state, move, player)) { points += 100; }

                if (points > maxPoints)
                {
                    maxPoints = points;
                    secondBestMove = bestMove ;
                    bestMove = move;
                }
            }


            //Kies heel soms niet de beste, maar de tweede . 
            //Dit zorgt ervoor dat het testen van Engine tegen zichzelf, niet telkens hetzelfde game verloop geeft.
            if (MediumPointBasedPlayer.rnd.NextDouble() > 0.99)
            {
                return secondBestMove;
            }

            return bestMove;
        }
    }



    /// <summary>
    /// Helper class for the MediumPointBasedPlayer to simulate moves and their consequences
    /// </summary>
    class Simulator
    {


        public static int InversePlayer(int player)
        {
            if (player == 1) { return 2; } else { return 1; }
        }

        public static bool IsWinningMove(Board board, Move move, int player)
        {

            var sb = board.SmallBoards[move.BoardColumn, move.BoardRow];
            var clonedsb = sb.Clone();
            clonedsb.SetOwner(move.CellColumn, move.CellRow, player);

            return clonedsb.IsWinner(player);
        }

        public static bool SmallBoardAlreadyWonByComponent(Board board, Move move, int player)
        {
            var sb = board.SmallBoards[move.CellColumn, move.CellRow];
            return (sb.IsWinner(Simulator.InversePlayer(player)));
        }

        public static bool DestinationBoardIsFull(Board board, Move move)
        {
            var sb = board.SmallBoards[move.CellColumn, move.CellRow];
            return sb.IsFull();
        }


        public static bool IsCorner(Move move)
        {
            return ((move.CellColumn == 0) && (move.CellRow == 0)) ||
             ((move.CellColumn == 2) && (move.CellRow == 0)) ||
             ((move.CellColumn == 0) && (move.CellRow == 2)) ||
             ((move.CellColumn == 2) && (move.CellRow == 2));
        }

        public static bool IsCenter(Move move)
        {
            return ((move.CellColumn == 1) && (move.CellRow == 1));
        }

    }


}
