using System;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string str)
    {
        return String.IsNullOrEmpty(str);
    }
}