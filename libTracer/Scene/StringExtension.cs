using System.Collections.Generic;
using System;

namespace libTracer.Scene;

internal static class StringExtension
{
    public static IEnumerable<String> SplitToLines(this String input)
    {
        if (input == null)
        {
            yield break;
        }

        using System.IO.StringReader reader = new System.IO.StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}
