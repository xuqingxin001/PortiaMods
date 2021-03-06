﻿using System.Collections.Generic;

public static class ListExtensions
{
    public static int InsertSorted<T>(this List<T> @this, T item, IComparer<T> comparer)
    {
        if (@this.Count == 0 || comparer.Compare(@this[@this.Count - 1], item) <= 0)
        {
            @this.Insert(@this.Count, item);
            return @this.Count;
        }

        if (comparer.Compare(@this[0], item) >= 0)
        {
            @this.Insert(0, item);
            return 0;
        }

        var index = @this.BinarySearch(item, comparer);
        if (index < 0) index = ~index;
        @this.Insert(index, item);
        return index;
    }

    public static List<T> Copy<T>(this List<T> @this) => @this.ConvertAll(_ => _);
}