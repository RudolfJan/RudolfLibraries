using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Library.TextHelpers
  {
  public class TimeConverters
    {
    public static string SecondsToString(ulong seconds, bool showSeconds)
      {
      var minutes = seconds / 60;
      var hours = minutes / 60;
      minutes = minutes - hours * 60;
      var remainder = seconds - hours * 3600 - minutes * 60;
      var output= $"{hours:D2}:{minutes:D2}";
      if (showSeconds)
        {
        output += $":{remainder:D2}";
        }
      return output;
      }

    // Input format should be hh:mm:ss or hh:mm
    // No localization, only 24h format
    public static ulong TimeStringToSeconds(string time)
      {
      string[] s1 = time.Split(":");
      if (s1.Length < 2)
        {
        throw new FormatException(
          $"String format for time is not correct should be hh:mm:ss but is {time}");
        }
      var success1 = ulong.TryParse(s1[0], out var hours);
      var success2= ulong.TryParse(s1[1], out var minutes);
      ulong seconds = 0;
      bool success3=true;
      if (s1.Length > 2)
        {
        success3=ulong.TryParse(s1[2], out seconds);
        }

      success1 = success1 && hours < 24;
      success2 = success2 && minutes < 60;
      success3 = success3 && seconds < 60;
      if (!success1 || !success2 || !success3)
        {
        throw new FormatException(
          $"String format for time is not correct should be hh:mm:ss but is {time}");
        }

      return (hours*3600+minutes*60+seconds);
      }

    public static bool IsValidTimeString(string time)
      {
      string[] s1 = time.Split(":");
      if (s1.Length < 2)
        {
        return false;
        }

      foreach (var c in s1[0])
        {
        if (!char.IsDigit(c))
          {
          return false;
          }
        }

      foreach (var c in s1[1])
        {
        if (!char.IsDigit(c))
          {
          return false;
          }
        }
 
      if (s1.Length > 2)
        {
        foreach (var c in s1[2])
          {
          if (!char.IsDigit(c))
            {
            return false;
            }
          var value2 = Convert.ToUInt64(s1[1]);
          if (value2 > 59)
            {
            return false;
            }
          }
        }

      if (s1.Length > 2)
        {
        return false;
        }
      var value = Convert.ToUInt64(s1[0]);
      if (value > 23)
        {
        return false;
        }
      value = Convert.ToUInt64(s1[1]);
      if (value > 59)
        {
        return false;
        }

      return true;
      }
    }
  }
