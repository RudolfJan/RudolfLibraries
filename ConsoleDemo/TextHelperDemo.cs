using System;
using System.Text;
using Utilities.Library.TextHelpers;

namespace ConsoleDemo
{
  public class TextHelperDemo
  {
    public static void TestHelperDemoSamples()
    {
      Console.WriteLine("\r\nText helpers demo");
      AddbackslashDemo();
      QuoteFileNameTest();
      HexViewerDemo();
    }
    public static void AddbackslashDemo()
    {
      string input1 = "C:\\Test";
      Console.WriteLine($"Add backslash to {input1} becomes {TextHelper.AddBackslash(input1)}");

      string input2 = "C:\\Test\\";
      Console.WriteLine($"Add backslash to {input2} becomes {TextHelper.AddBackslash(input2)}");

      string input3 = "C:\\Test with a space";
      Console.WriteLine($"Add backslash to {input3} becomes {TextHelper.AddBackslash(input3)}");
    }

    public static void QuoteFileNameTest()
    {
      string input1 = "C:\\Test";
      Console.WriteLine($"Quote file name {input1} becomes {TextHelper.QuoteFilename(input1)}");

      string input2 = "C:\\Test with space part";
      Console.WriteLine($"Quote file name {input2} becomes {TextHelper.QuoteFilename(input2)}");

      string input3 = "C:\\Test with a space\\And more spaces.jpg";
      Console.WriteLine($"Quote file name {input3} becomes {TextHelper.QuoteFilename(input3)}");

      string input4 = "\"C:\\Already quotes\\And more spaces.jpg\"";
      Console.WriteLine($"Quote file name {input4} becomes {TextHelper.QuoteFilename(input4)}");

    }

    public static void HexViewerDemo()
    {
      byte[] simpleString = Encoding.Default.GetBytes("This is a simple valid text string");
      byte[] empty = Array.Empty<byte>();
      byte[] nullsOnly = new byte[16];
      byte[] allValues = new byte[255];
      for (int i = 0; i < 255; i++)
      {
        allValues[i] = (byte)i;
      }

      Console.WriteLine($"Empy:\r\n{TextHelper.HexViewer(empty)}\r\n\r\n");
      Console.WriteLine($"Simple string:\r\n{TextHelper.HexViewer(simpleString)}\r\n\r\n");
      Console.WriteLine($"Nulls only:\r\n{TextHelper.HexViewer(nullsOnly)}\r\n\r\n");
      Console.WriteLine($"all values:\r\n{TextHelper.HexViewer(allValues)}\r\n\r\n");
      Console.WriteLine($"short line:\r\n{TextHelper.HexViewer(simpleString, 4)}\r\n\r\n");

    }
  }
}
