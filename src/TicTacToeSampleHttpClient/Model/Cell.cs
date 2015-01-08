namespace TicTacToeSampleHttpClient.Model
{
    public class Cell
    {
        /// <summary>
        /// The owner of the cell.
        /// Owner = 0 if the cell is free
        /// Owner = 1 for player1 
        /// Owner = 2 for player2
        /// </summary>
        public int Owner { get; set; }

        public int Column { get; set; }
        public int Row { get; set; }

        public Cell(int column, int row)
        {
            Column = column;
            Row = row;
            Owner = 0;
        }

        public Cell Clone()
        {
            var result = new Cell(Column, Row) { Owner = Owner };
            return result;
        }
    }
}