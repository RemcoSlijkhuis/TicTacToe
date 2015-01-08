using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TicTacToeShared
{
    public static class MovesExtensions
    {
        /// <summary>
        /// Gets all the available moves 
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        public static string ToMovesString(this List<Move> moves)
        {
            var availableMoves = new HashSet<string>(moves.Select(x => x.BoardColumn.ToString(CultureInfo.InvariantCulture) + x.BoardRow.ToString(CultureInfo.InvariantCulture) +
                                                                       x.CellColumn.ToString(CultureInfo.InvariantCulture) + x.CellRow.ToString(CultureInfo.InvariantCulture)));

            var moveBuilder = new StringBuilder();
            for (var sbcolumn = 0; sbcolumn < 3; sbcolumn++)
            {
                for (var sbrow = 0; sbrow < 3; sbrow++)
                {

                    for (var x = 0; x < 3; x++)
                    {
                        for (var y = 0; y < 3; y++)
                        {
                            if (availableMoves.Contains(sbcolumn.ToString(CultureInfo.InvariantCulture) + sbrow.ToString(CultureInfo.InvariantCulture) +
                                                        x.ToString(CultureInfo.InvariantCulture) + y.ToString(CultureInfo.InvariantCulture)))
                                moveBuilder.Append("Y");
                            else
                                moveBuilder.Append("N");
                        }
                    }
                }
            }

            return moveBuilder.ToString();
        }
    }
}
