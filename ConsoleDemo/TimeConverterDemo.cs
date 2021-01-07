using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Library.TextHelpers;

namespace ConsoleDemo
  {
  public class TimeConverterDemo
    {
    public static void TimeConverterDemoApp()
      {

      Test("01:02:03", true);
      Test("00:23:00", true);
      Test("04:20", false);
      Test("02:00", true);
      Test("23:59:59", true);
      TryTest("");
      TryTest("25:00");
      TryTest("0A:00");
      TryTest("00:65");
      TryTest("00:00:65");
      TryTest("00:00:FF");
      }

    private static void TryTest(string input)
      {
      try
        {
        Console.WriteLine($"Time string {input} validity is {TimeConverters.IsValidTimeString(input)}");
        ulong seconds = TimeConverters.TimeStringToSeconds(input);
        }
      catch (Exception ex)
        {
        Console.WriteLine($"Exception should happen: {ex.Message}");
        }
      }

    private static void Test(string input, bool showSeconds)
      {
      Console.WriteLine($"Time string {input} validity is {TimeConverters.IsValidTimeString(input)}");
      ulong seconds = TimeConverters.TimeStringToSeconds(input);
      var output = TimeConverters.SecondsToString(seconds, showSeconds);
      if (String.CompareOrdinal(input, output) == 0)
        {
        Console.WriteLine($"OK for {output}");
        }
      }
    }
  }
