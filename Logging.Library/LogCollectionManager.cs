using System;
using System.Collections.Generic;

namespace Logging.Library
  {
  public static class LogCollectionManager
    {
    public static List<LogEntryClass> LogEvents { get; set; } = new List<LogEntryClass>();
    static LogCollectionManager()
      {
      LogEventHandler.LogEvent += OnSaveLogEvent;
      }

    public static void OnSaveLogEvent(Object Sender, LogEventArgs E)
      {
      LogEvents.Add(E.EntryClass);
      }

    public static List<string> ReportLog()
      {
      var output=new List<string>();

      foreach (var logEntry in LogEvents)
        {
        var s = $"{logEntry.Method}: {logEntry.LineNumber} {logEntry}";
        output.Add(s);
        }
      return output;
      }

    }
  }
