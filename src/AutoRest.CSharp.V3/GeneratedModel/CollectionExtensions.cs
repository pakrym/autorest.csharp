using System;
using System.Collections.Generic;

namespace AutoRest.CSharp.V3
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable ?? Array.Empty<T>();
        }
    }
}