using System.Threading;

namespace TicTacToeShared
{
    /// <summary>
    /// Stats class to keep track of which player is winning or losing.
    /// All operations in this class are thread safe
    /// </summary>
    public class Stats
    {
        public int NumberOfGames
        {
            get { return numberOfGames; }
        }
        private int numberOfGames;

        public int Player1Wins
        {
            get { return player1Wins; }
        }
        private int player1Wins;

        public int Player2Wins
        {
            get { return player2Wins; }
        }
        private int player2Wins;

        public int Draws
        {
            get { return draws; }
        }
        private int draws;

        public long Player1Duration
        {
            get { return player1Duration; }
        }
        private long player1Duration;

        public long Player2Duration
        {
            get { return player2Duration; }
        }
        private long player2Duration;
        
        public void AddResult(int gameResult, long duration1 = 0, long duration2 = 0)
        {
            Interlocked.Increment(ref numberOfGames);
            if (gameResult == 1) { Interlocked.Increment(ref player1Wins); }
            if (gameResult == 2) { Interlocked.Increment(ref player2Wins); }
            if (gameResult == 3) { Interlocked.Increment(ref draws); }
            
            long initialValue, computedValue;
            
            do
            {
                initialValue = player1Duration;

                computedValue = initialValue + duration1;
            }
            while (initialValue != Interlocked.CompareExchange(ref player1Duration, computedValue, initialValue));

            do
            {
                initialValue = player2Duration;

                computedValue = initialValue + duration2;
            }
            while (initialValue != Interlocked.CompareExchange(ref player2Duration, computedValue, initialValue));
        }

        public void Clear()
        {
            Interlocked.Exchange(ref numberOfGames, 0); 
            Interlocked.Exchange(ref player1Wins, 0);
            Interlocked.Exchange(ref player2Wins, 0);
            Interlocked.Exchange(ref draws, 0);
            Interlocked.Exchange(ref player1Duration, 0);
            Interlocked.Exchange(ref player2Duration, 0);
        }
    }
}