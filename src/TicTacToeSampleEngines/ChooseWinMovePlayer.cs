using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeShared;

namespace TicTacToeSampleEngines
{

    /// <summary>
    /// Sample engine.
    /// Will choose a winning smallboard move whenever possible.
    /// Otherwise just a random move.
    /// </summary>
    public class ChooseWinFieldPlayer : IPlayerEngine
    {

        private static Random rnd = new Random();

        public void BeforeGame() {
          //nothing to initialize before a game
        }
        public void AfterGame(int player, Board board, List<Move> moves) { 
         
        }


        public Move GetMove(int player, Board board, List<Move> moves)
        {

            var availMoves = board.GetAvailableMoves();
         
            //zit er een move bij zodat ik win ?
            foreach (var move in availMoves)
            {
                //haal het smallboard erbij van deze beschikbare move. 
                var sb = board.SmallBoards[move.BoardColumn, move.BoardRow];

                //maak een clone van geselecteerde smallboard om een move te kunnen simuleren.
                var ClonedSmallboard = sb.Clone();

                //maak onszelf eigenaar van het cell
                ClonedSmallboard.SetOwner(move.CellColumn, move.CellRow, player);

                //controleer of ik nu winnaar ben van het smallboard
                if (ClonedSmallboard.IsWinner(player))
                {
                    //zo ja, kies dan deze move !
                    return move;
                }

                
            }
           

            //anders een willekeurig move
            int randomindex = ChooseWinFieldPlayer.rnd.Next(availMoves.Count);
            return availMoves[randomindex];

        }
    }
}
