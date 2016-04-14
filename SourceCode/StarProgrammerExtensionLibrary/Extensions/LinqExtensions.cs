using System;
using System.Collections.Generic;
using System.Linq;

namespace StarProgrammerExtensionLibrary.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));

            //foreach (var element in source)
            //{
            //	if (seenKeys.Add(keySelector(element)))
            //	{
            //		yield return element;
            //	}
            //}
        }
    }
}