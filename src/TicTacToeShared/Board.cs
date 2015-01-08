using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeShared
{

    /// <summary>
    /// The bigger board holding the 3x3 smallboards
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The column/row based [0..2,0..2] array of the smallboards.
        /// First column, then row. 
        /// </summary>
        public SmallBoard[,] SmallBoards { get; set; }


        /// <summary>
        /// GameWinner=1 when player1 wins
        /// GameWinner=2 when player2 wins
        /// GameWinner=3 when there is a draw
        /// </summary>
        public int GameWinner { get; set; }

        /// <summary>
        /// The column on the bigboard you must use in your next move.
        /// If it is -1 you can pick your own smallboard.
        /// </summary>
        public int NextBoardColumn { get; set; }

        /// <summary>
        /// The row on the bigboard you must use in your next move.
        /// If it is -1 , you can pcik your own smallboard.
        /// </summary>
        public int NextBoardRow { get; set; }


        public Board()
        {
            this.SmallBoards = new SmallBoard[3, 3];
            NextBoardColumn = -1;
            NextBoardRow = -1;
        }
        
        /// <summary>
        /// Initialize / clears a board. 
        /// </summary>
        public void Init()
        {
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    SmallBoards[x, y] = new SmallBoard(x, y);
                    SmallBoards[x, y].EmptyCells();
                }
            }
        }
        
        /// <summary>
        /// Executes a move on the board
        /// Validates and checks the move.
        /// </summary>
        public void ExecuteMove(Move move, int player)
        {
            if (!move.ConstraintsAreValid())
            {
                throw new Exception("Invalid move : values out of range");
            }

            //if Board requires a smallboard column/row , double check  
            if (NextBoardColumn != -1)
            {
                if ((NextBoardColumn != move.BoardColumn) ||
                    (NextBoardRow != move.BoardRow))
                {
                    throw new Exception("Invalid move : wrong required board in move");
                }
            }

            //check weither or not the cell is already taken
            var smallboard = SmallBoards[move.BoardColumn, move.BoardRow];
            if (smallboard.GetOwner(move.CellColumn, move.CellRow) != 0)
            {
                throw new Exception("Invalid move : cell is already taken!");
            }

            FastExecuteMove(move, player);
        }


        /// <summary>
        /// Execute a move on this board. 
        /// Does not validate/check the rules. Use this function in your simulation.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="player"></param>
        public void FastExecuteMove(Move move, int player)
        {
            //check weither or not the cell is already taken
            var smallboard = SmallBoards[move.BoardColumn, move.BoardRow];

            var wasAlreadyWinner = smallboard.IsWinner(player);
            smallboard.SetOwner(move.CellColumn, move.CellRow, player);

            //opeens winnaar geworden? dan complete board winner opnieuw uitrekenen. 
            if (!wasAlreadyWinner & smallboard.IsWinner(player))
            {
                //there was a change in winner value, update complete board winner
                CalculateBoardWinner();
            }

            CheckDraw();

            NextBoardColumn = move.CellColumn;
            NextBoardRow = move.CellRow;

            //if smallBoard is full , then let next move freely decide what todo
            var nextSb = SmallBoards[NextBoardColumn, NextBoardRow];
            if (nextSb.IsFull())
            {
                NextBoardColumn = -1;
                NextBoardRow = -1;
            }
            
            //also , in SingleBoard modes (default rule), next player can freely choose
            if (Game.RuleMode == RulesModes.SingleWinSmallBoard) 
            {
                if (nextSb.HasWinner())
                {
                    NextBoardColumn = -1;
                    NextBoardRow = -1;
                }
            }
        }
        
        /// <summary>
        /// Generates a list of available moves
        /// </summary>
        /// <returns></returns>
        public List<Move> GetAvailableMoves()
        {
            var moves = new List<Move>();
            
            //check of ik zelf nog een SmallBoard moet zoeken 
            if (NextBoardColumn == -1)
            {
                for (var x = 0; x < 3; x++)
                {
                    for (var y = 0; y < 3; y++)
                    {
                        if (Game.RuleMode == RulesModes.MultiWinSmallBoard)
                        {
                            throw new NotImplementedException();
                        }

                        //assuming SingleWinSmallBoard rule (default)
                        //In this case , the smallboard must not be full, but also : cannot have a winner.
                        if (!SmallBoards[x, y].IsFull() && !SmallBoards[x,y].HasWinner())
                        {
                            var sb = SmallBoards[x, y];
                            var frees = sb.GetAvailableCells();

                            for (var i = 0; i < frees.Count; i++)
                            {
                                moves.Add(new Move(sb.Column, sb.Row, frees[i].Column, frees[i].Row));
                            }
                        }
                    }
                }
            }
            else
            {
                var sb = SmallBoards[NextBoardColumn, NextBoardRow];
                var frees = sb.GetAvailableCells();
                for (var i = 0; i < frees.Count; i++)
                {
                    moves.Add(new Move(sb.Column, sb.Row, frees[i].Column, frees[i].Row));
                }
            }
            return moves;
        }


        /// <summary>
        /// Make a deep clone of this board. Used by Game to present a copy to the engines.
        /// </summary>
        /// <returns>A cloned board</returns>
        public Board Clone()
        {
            var result = new Board { SmallBoards = new SmallBoard[3, 3] };

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result.SmallBoards[x, y] = SmallBoards[x, y].Clone();
                }
            }

            result.NextBoardColumn = NextBoardColumn;
            result.NextBoardRow = NextBoardRow;
            result.GameWinner = GameWinner;

            return result;
        }


        /// <summary>
        /// Checks if board is in a "draw" state.
        /// No more moves available.
        /// </summary>
        private void CheckDraw()
        {
            if (GameWinner != 0) return;

            var fullSmallboards = 0;
            for (var bc = 0; bc < 3; bc++)
            {
                for (var br = 0; br < 3; br++)
                {
                    if (Game.RuleMode == RulesModes.SingleWinSmallBoard)
                    {
                        if (SmallBoards[bc, br].IsFull() || SmallBoards[bc,br].HasWinner() )
                        {
                            fullSmallboards++;
                        }
                    }
                    else
                    {
                        if (SmallBoards[bc, br].IsFull())
                        {
                            fullSmallboards++;
                        }
                    }
                   
                }
            }
            if (fullSmallboards == 9)
            {
                GameWinner = 3;
            }
        }
        
        private void CalculateBoardWinner()
        {
            var winner = 0;

            //Een keertje voor speler 1 en daarna voor speler 2 kijken
            //dat moet in een loopje vanwege GameRule multiwinner 

            for (var pi = 1; pi < 3; pi++)
            {
                //vertical 3x times
                for (var x = 0; x < 3; x++)
                {
                    if (SmallBoards[x, 0].IsWinner(pi) &&
                        SmallBoards[x, 1].IsWinner(pi) &&
                        SmallBoards[x, 2].IsWinner(pi))
                    {
                        winner = pi;
                    }
                }

                //horizontal 3 times
                for (var x = 0; x < 3; x++)
                {
                    if (SmallBoards[0, x].IsWinner(pi) &&
                       SmallBoards[1, x].IsWinner(pi) &&
                       SmallBoards[2, x].IsWinner(pi))
                    {
                        winner = pi;
                    }
                }

                //diag leftupper rightbottom
                if (SmallBoards[0, 0].IsWinner(pi) &&
                    SmallBoards[1, 1].IsWinner(pi) &&
                    SmallBoards[2, 2].IsWinner(pi))
                {
                    winner = pi;
                }

                //diag rightupper leftbottom
                if (SmallBoards[2, 0].IsWinner(pi) &&
                    SmallBoards[1, 1].IsWinner(pi) &&
                    SmallBoards[0, 2].IsWinner(pi))
                {
                    winner = pi;
                }
            }

            GameWinner = winner;
        }
    }
}
