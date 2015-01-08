using System;
using TicTacToeShared;

namespace TicTacToeServer
{
    /// <summary>
    /// Represents the state of a specific player
    /// </summary>
    internal class ServerGameState
    {
        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The timestamp of when the game was last touched
        /// </summary>
        public DateTime LastMove { get; set; }

        /// <summary>
        /// The player that's currently starting (will always be the server initially)
        /// </summary>
        public int CurrentStartingPlayer { get; set; }

        /// <summary>
        /// The current game being played
        /// </summary>
        public Game CurrentGame { get; private set; }

        /// <summary>
        /// Player 1 (always the server)
        /// </summary>
        public IPlayerEngine Player1 { get; private set; }

        /// <summary>
        /// Player 2 (always the client)
        /// </summary>
        public ClientPlayer Player2 { get; private set; }

        /// <summary>
        /// The statistics for this player
        /// </summary>
        public Stats Stats { get; private set; }

        public void StartNewGame()
        {
            CurrentGame = new Game(Player1, Player2, CurrentStartingPlayer);
            SwapStartingPlayer();
        }

        private void SwapStartingPlayer()
        {
            CurrentStartingPlayer = CurrentStartingPlayer == 1 ? 2 : 1;
        }

        public ServerGameState()
        {
            LastMove = DateTime.Now;
            CurrentStartingPlayer = 1;
            Player1 = Activator.CreateInstance(GameStateHelper.ServerEngineType) as IPlayerEngine;
            Player2 = new ClientPlayer();
            Stats = new Stats();
        }
    }
}
