using System;
using System.Linq;

namespace TicTacToeSampleHttpClient.Model
{
    /// <summary>
    /// Represents a move on the board.
    /// The Board Col/Row are used to identify which of 9 smallboards of the big board was choosen. 
    /// The Cell Col/Row are used to indentify the used cell within that smallboard.
    /// </summary>
    public class Move
    {
        public int BoardColumn { get; set; }
        public int BoardRow { get; set; }
        public int CellColumn { get; set; }
        public int CellRow { get; set; }

        //Just a handy tool to store information in moves 
        public Object Tag;

        public Move(string state)
        {
            if(state.Length != 4)
                throw new ArgumentException("state is not correct (should be 4 chars)", state);

            var moveState = state.ToCharArray().Select(x => (x - '0')).ToList();

            BoardColumn = moveState[0];
            BoardRow = moveState[1];
            CellColumn = moveState[2];
            CellRow = moveState[3];
        }

        public bool ConstraintsAreValid()
        {
            return !CheckBoardAndCell();
        }

        private bool CheckBoardAndCell()
        {
            return (this.BoardColumn < 0) || (this.BoardColumn > 2) || (this.BoardRow < 0) || (this.BoardColumn > 2) ||
                   (this.CellColumn < 0) || (this.CellColumn > 2) || (this.CellRow < 0) || (this.CellRow > 2);
        }

        public override string ToString()
        {
            return string.Concat(BoardColumn, BoardRow, CellColumn, CellRow);
        }
    }
}