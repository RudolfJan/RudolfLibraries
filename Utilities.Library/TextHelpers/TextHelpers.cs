using System;
using System.Collections.Generic;
using System.Globalization;
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


    // This is a very simple viewer for byte strings.
    public static string HexViewer(byte[] input, int lineLength = 16)
      {
      var output = "";
      var inputLength = input.Length;
      StringBuilder hexString= new StringBuilder();
      StringBuilder charString= new StringBuilder();
      for (int i = 0; i < inputLength / lineLength; i++)
        {
        hexString.Clear();
        charString.Clear();
        var position = (i * lineLength).ToString("0000");
        for (int j = 0; j < lineLength; j++)
          {
          var value = input[i * lineLength + j];
          var result = char.IsLetterOrDigit((char) value) || char.IsPunctuation((char) value) ||
                        char.IsSymbol((char) value) || ((char) value == ' ');
          if (result)
            {
            charString.Append((char) value);
            }
          else
            {
            charString.Append('.');
            }

          byte[] value2 = new byte[1];
          value2[0]= value;
          hexString.Append(BitConverter.ToString(value2).Replace("-", " ")).Append(' ');
          }

        output += position + "  "+  hexString + "    " + charString + "\r\n";
        }
      return output;
      }

    // This function translates the C# wildcard symbols * and ? to % and _ and will escape % and _ by a backslash
    public static string ToLikeWildCard(string input)
      {
      var output= input.Replace("%","\\%").Replace("_","\\_");
      output = output.Replace("*", "%").Replace("?", "_");
      return output;
      }

    }
  }
