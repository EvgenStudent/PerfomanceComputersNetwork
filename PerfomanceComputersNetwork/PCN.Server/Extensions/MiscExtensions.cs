using System;
using System.Collections.Generic;
using System.Linq;

namespace PCN.Server.Extensions
{
    public static class MiscExtensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int take)
        {
            var enumerable = source as T[] ?? source.ToArray();
            return enumerable.Skip(Math.Max(0, enumerable.Length - take));
        }
    }
}