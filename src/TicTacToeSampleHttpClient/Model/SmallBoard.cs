using System;

namespace TicTacToeSampleHttpClient.Model
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

        //0= no winner
        //1 = player1
        //2 = player2
        //3 = both player1 and player2 won this Smallboard
        private int winner = 0;

        public bool IsWinner(int player)
        {
            return ((winner & player) == player);
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
                //only check for winners if cells filled are more then 2 

                if ((winner & player) != player)
                {
                    CalculateWinnerBasedOnLastMove(column, row, player);
                }
            }
        }

        private void CalculateWinnerBasedOnLastMove(int column, int row, int player)
        {

            if (true) // Different game rules?
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
            if ((column + row) == 2)
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
    }
}