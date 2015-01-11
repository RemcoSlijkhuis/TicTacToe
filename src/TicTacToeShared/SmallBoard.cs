using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeShared
{
    public class SmallBoard
    {
        /// <summary>
        /// The column (0..2) of the position of this smallboard within the bigboard. 
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// The row (0..2) of the position of this smallboard within the bigboard.
        /// </summary>
        public int Row { get; private set; }

        public Cell[,] GetCells
        {
            get
            {
                return cells;
            }
        }
        private readonly Cell[,] cells;

        //0= no winner
        //1 = player1
        //2 = player2
        //3 = both player1 and player2 won this Smallboard
        private int winner = 0;
        
        private int cellsFilled;


        public SmallBoard(int column, int row)
        {
            Column = column;
            Row = row;
            cells = new Cell[3, 3];
        }

        public void EmptyCells()
        {
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }
        }

        public int GetOwner(int column, int row)
        {
            return cells[column, row].Owner;
        }

        public void SetOwner(int column, int row, int player)
        {
            var c = cells[column, row];

            if (player == 0)
            {
                throw new Exception("Cannot undo cell owner change ! Try using a cloned version of this smallboard for move tests.");
            }

            if (c.Owner != player)
            {
                c.Owner = player;
                cellsFilled++;
                //only check for winners if cells filled are more then 2 
                if (cellsFilled > 2)
                {
                    //only check for winner if this player is not already a winner
                    if ((winner & player) != player)
                    {
                        CalculateWinnerBasedOnLastMove(column,row,player);
                    }
                }
            }
        }

        public bool IsWinner(int player)
        {
            return ((winner & player) == player);
        }

        private void CalculateWinnerBasedOnLastMove(int column, int row, int player)
        {
            
            if (Game.RuleMode == RulesModes.SingleWinSmallBoard)
            {
                //in SingleWinSmallboard modes, kan er maar één winnaar zijn . 
                //dus direct returnen als blijkt dat er al een winnaar is.
                if (winner != 0) { return; }
            }
                
            //check vertial 
            if ((cells[column, 0].Owner == player) && (cells[column, 1].Owner == player) && (cells[column, 2].Owner == player))
            {
                winner = winner | player;
                return;
            }

            //check horizontal
            if ((cells[0, row].Owner == player) && (cells[1, row].Owner == player) && (cells[2, row].Owner == player))
            {
                winner = winner | player;
                return;
            }

            //is Diagonal ?
            if ((column + row) % 2 == 0)
            {
                if ((cells[0, 0].Owner == player) && (cells[1, 1].Owner == player) && (cells[2, 2].Owner == player))
                {
                    winner = winner | player;
                    return;
                }
                if ((cells[0, 2].Owner == player) && (cells[1, 1].Owner == player) && (cells[2, 0].Owner == player))
                {
                    winner = winner | player;
                    return;
                }
            }
        }


        /// <summary>
        /// Indicates weither or not this smallboard has available cells
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return (cellsFilled == 9);
        }

        /// <summary>
        /// Indicates weither or not this smallboard has a winner.
        /// </summary>
        /// <returns></returns>
        public bool HasWinner()
        {
            return winner != 0;
        }

        public List<Cell> GetAvailableCells()
        {
            var result = new List<Cell>();
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    var cell = this.cells[x, y];

                    if (cell.Owner == 0)
                    {
                        result.Add(cell);
                    }
                }
            }

            return result;
        }

        //generate a "cloned" version of this singleboard
        //used to provide the Smallboard to the PlayerEngine and prefend "changes" to the internal state of the game.
        public SmallBoard Clone()
        {
            var result = new SmallBoard(Column, Row);
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result.cells[x, y] = cells[x, y].Clone();
                }
            }

            result.winner = winner;
            result.cellsFilled = cellsFilled;
            return result;
        }
    }
}
