using System;

namespace TicTacToeShared
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

        public Move(int boardColumn, int boardRow, int cellColumn, int cellRow)
        {
            BoardColumn = boardColumn;
            BoardRow = boardRow;
            CellColumn = cellColumn;
            CellRow = cellRow;
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
    }
}