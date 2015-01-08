using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeShared
{
    public class Game
    {
        /// <summary>
        /// Global setting for determine which rule is used
        /// See RulesModes for more explanation.
        /// </summary>
        public static RulesModes RuleMode = RulesModes.SingleWinSmallBoard;

        private readonly IPlayerEngine player1;
        private readonly IPlayerEngine player2;
        private IPlayerEngine currentPlayer;
        
        public int CurrentPlayerIndex
        {
            get { return currentPlayerIndex; }
            set
            {
                if ((value == 1) || (value == 2))
                {
                    currentPlayerIndex = value;
                    if (value == 1)
                    {
                        currentPlayer = player1;
                    }
                    else
                    {
                        currentPlayer = player2;
                    }
                }

            }
        }
        private int currentPlayerIndex;


        public Board Board { get; private set; }
        public List<Move> Moves { get; private set; }

        /// <summary>
        /// See Board.Winner for more details
        /// </summary>
        public int GameResult
        {
            get
            {
                return Board.GameWinner;
            }
        }


        public Game(IPlayerEngine player1, IPlayerEngine player2, int startingPlayerIndex)
        {
            this.player1 = player1;
            this.player2 = player2;

            Init(startingPlayerIndex);
        }

        public Game(IPlayerEngine player1, IPlayerEngine player2, string gameState)
        {
            this.player1 = player1;
            this.player2 = player2;
            
            Init(-1, gameState);
        }
        
        public void Init(int startingPlayerIndex, string gameState = "")
        {
            Board = new Board();
            Board.Init();

            Moves = new List<Move>();
           
            if (gameState != "")
            {
                // Find out which player has the most moves, the opposite player will have the turn
                var startingPlayer = gameState.ToCharArray()
                                              .Select(x => (x - '0'))
                                              .GroupBy(x => x)
                                              .Where(x => x.Key > 0)
                                              .OrderBy(x => x.Count())
                                              .Select(x => x.Key)
                                              .FirstOrDefault();

                CurrentPlayerIndex = startingPlayer;

                var boards = gameState.ToCharArray().ToChunks(9).ToList();

                if (boards.Count != 9)
                    throw new InvalidOperationException("Invalid gamestate");

                for (var sbcolumn = 0; sbcolumn < 3; sbcolumn++)
                {
                    for (var sbrow = 0; sbrow < 3; sbrow++)
                    {
                        var boardState = boards[sbcolumn*3 + sbrow];

                        var board = Board.SmallBoards[sbcolumn, sbrow];
                        var boardMoves = boardState.Select(x => (x - '0')).ToList();

                        for (var x = 0; x < 3; x++)
                        {
                            for (var y = 0; y < 3; y++)
                            {
                                var playerMove = boardMoves[x*3 + y];

                                if (playerMove <= 0) continue;
                                if(playerMove < 0 || playerMove > 2)
                                    throw new InvalidOperationException("Invalid gamestate");

                                board.SetOwner(x, y, playerMove);
                                Moves.Add(new Move(sbcolumn, sbrow, x, y));
                            }
                        }
                    }
                }
            }
            else
            {
                CurrentPlayerIndex = startingPlayerIndex;

                player1.BeforeGame();
                player2.BeforeGame();    
            }
        }

        public void SwapPlayer()
        {
            CurrentPlayerIndex = CurrentPlayerIndex == 1 ? 2 : 1;
        }

        public void PlayOneMove()
        {
            //don't play if game is finished 
            if (GameResult != 0)
                return;

            //make a cloned version of the board, to give to the IPlayerEngine
            var state = Board.Clone();
            var move = currentPlayer.GetMove(this.CurrentPlayerIndex, state, Moves);

            Board.ExecuteMove(move, CurrentPlayerIndex);
            Moves.Add(move);
            SwapPlayer();

            if (Board.GameWinner != 0)
            {
                player1.AfterGame(1, Board, Moves);
                player2.AfterGame(2, Board, Moves);
            }
        }

        public override string ToString()
        {
            var gameStateBuilder = new StringBuilder();
            for (var sbcolumn = 0; sbcolumn < 3; sbcolumn++)
            {
                for (var sbrow = 0; sbrow < 3; sbrow++)
                {
                    var board = Board.SmallBoards[sbcolumn, sbrow];

                    for (var x = 0; x < 3; x++)
                    {
                        for (var y = 0; y < 3; y++)
                        {
                            gameStateBuilder.Append(board.GetCells[x, y].Owner);
                        }
                    }
                }
            }

            return gameStateBuilder.ToString();
        }

        public static Game FromString(string gameState, IPlayerEngine player1, IPlayerEngine player2)
        {
            if(string.IsNullOrEmpty(gameState) || gameState.Length != 81)
                throw new ArgumentException("GameState is not valid!", "gameState");

            return new Game(player1, player2, gameState);
        }
    }
}
