using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace TicTacToeServer
{
    /// <summary>
    /// Provides in memory games
    /// </summary>
    internal static class GameStateHelper
    {
        public static long TotalRequestsServed
        {
            get { return totalRequestsServed; }
        }
        private static long totalRequestsServed;

        public static DateTime UpSince { get; private set; }

        public static ConcurrentDictionary<Guid, ServerGameState> Games { get; private set; }

        public static Type ServerEngineType { get; private set; }

        public static void RegisterNewPlayer(Guid playerId, string name)
        {
            var existingPlayer = Games.Where(x => x.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Select(x => x.Key).FirstOrDefault();
            if (existingPlayer != Guid.Empty)
            {
                ServerGameState oldGameState;
                Games.TryRemove(existingPlayer, out oldGameState);

                Logger.Current.Info("New reaplcing player: {0}. He's taking on the boss... again", name);
            }
            else
            {
                Logger.Current.Info("New player online: {0}. He's taking on the boss :)", name);
            }
                
            Games.TryAdd(playerId, new ServerGameState { Name = name });
        }

        public static void AddRequest()
        {
            Interlocked.Increment(ref totalRequestsServed);
        }

        static GameStateHelper()
        {
            Games = new ConcurrentDictionary<Guid, ServerGameState>();
            ServerEngineType = Type.GetType(ConfigurationManager.AppSettings["ServerImplementation"]);
            totalRequestsServed = 0;
            UpSince = DateTime.Now;
        }
    }
}
