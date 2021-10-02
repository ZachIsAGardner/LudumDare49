using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public static class ListExtensions
{
    public static T Random<T>(this List<T> list)
        => list[Utility.RandomInt(list.Count)];

    public static bool IsNullOrEmpty<T>(this List<T> list)
        => list == null || list.Count < 1;

    ///<summary>
    /// Gets whatever it can from start index plus the amount to try to grab.
    ///</summary>
    public static List<T> TryGetRange<T>(this List<T> list, int index, int count)
    {
        List<T> result = new List<T>();

        for (int i = index; i < index + count; i++)
        {
            if (i >= list.Count) break;
            result.Add(list[i]);
        }

        return result;
    }

    public static string Andify(this List<string> list)
    {
        return Ify(list, "and");
    }
    public static string Andify(this string[] arr)
    {
        return Ify(arr.ToList(), "and");
    }
    public static string Andify(this IEnumerable<string> enumerable)
    {
        return Ify(enumerable.ToList(), "and");
    }

    public static string Orify(this List<string> list)
    {
        return Ify(list, "or");
    }
    public static string Orify(this string[] arr)
    {
        return Ify(arr.ToList(), "or");
    }

    private static string Ify(List<string> list, string connector)
    {
        string result = string.Join(", ", list.ToArray());
        int last = result.LastIndexOf(',');

        if (last == -1) return result;

        result = result.Remove(last, 1).Insert(last, " " + connector);

        return result;
    }

    public static IList<T> Clone<T>(this IList<T> listToClone) where T: ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static List<T> Shuffle<T>(this List<T> list) where T: ICloneable
    {
        var dupe = list.Clone();

        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = dupe.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = dupe[k];
            dupe[k] = dupe[n];
            dupe[n] = value;
        }

        return (List<T>)dupe;
    }
}