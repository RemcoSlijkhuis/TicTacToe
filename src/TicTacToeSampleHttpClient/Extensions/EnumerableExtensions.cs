using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeSampleHttpClient.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the collection in chunks
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="enumerable">The collection to split in chunks</param>
        /// <param name="chunkSize">The size of the chunks</param>
        public static IEnumerable<IEnumerable<T>> ToChunks<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            var itemsReturned = 0;
            var list = enumerable.ToList();
            var count = list.Count;

            while (itemsReturned < count)
            {
                var currentChunkSize = Math.Min(chunkSize, count - itemsReturned);
                yield return list.GetRange(itemsReturned, currentChunkSize);
                
                itemsReturned += currentChunkSize;
            }
        }
    }
}
