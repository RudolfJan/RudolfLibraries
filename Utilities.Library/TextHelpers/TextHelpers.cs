using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Library.TextHelpers
  {
  public class TextHelper
    {
    // Doubles all quotes in a string, needed to write the string to an SQL table, if input is null, will return null
    public static String DoubleQuotes(String Input)
      {
      return Input?.Replace("\"", "\"\"").Replace("\'", "\'\'");
      }

    // Add quotes to a filename in case it contains spaces.If the filepath is already quoted, don't do it again 
    public static string QuoteFilename(string s)
      {
      if (s.StartsWith("\"") && s.EndsWith("\""))
        {
        return s; // already quoted
        }
      else
        return $"\"{s}\"";
      }

    public static string AddBackslash(string input)
      {
      if (input.EndsWith("\\"))
        {
        return input;
        }

      return $"{input}\\";
      }

    }
  }
