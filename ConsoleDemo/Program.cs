using Logging.Library;
using System;

namespace ConsoleDemo
  {
  class Program
    {
    static void Main(string[] args)
      {
      // Setup logging
      LogEventHandler.LogEvent += ReportLogging;

      Console.WriteLine("Filter Demo");
      FiltersDemo.RunFilterDemo();
      Console.ReadLine();

      //Console.WriteLine("Tests for Time converters");
      //TimeConverterDemo.TimeConverterDemoApp();
      //Console.ReadLine();

      //Console.WriteLine("Text helper Demo");
      //TextHelperDemo.TestHelperDemoSamples();
      //Console.ReadLine();

      //Console.WriteLine("SQLite database Demo");
      //DataAccessDemo.SQLiteDatabaseDemo();
      //Console.ReadLine();

      //Console.WriteLine("TreeBuilder Demo");
      //TreeBuilderDemo.TreeDemo();
      //Console.ReadLine();

      //Console.WriteLine("SevenZip Demo");
      //SevenZipDemo.RunSevenZipDemo();
      //Console.ReadLine();

      var log =LogCollectionManager.ReportLog();
      if (log.Count == 0)
        {
        Console.WriteLine("Error log: Nothing logged");
        }
      Log.Trace("Test for logging");
      foreach (var line in log)
        {
        Console.WriteLine(line);
        }
      }

    private static void ReportLogging(Object Sender, LogEventArgs E)
      {
      Console.WriteLine($"{E.EntryClass.Method}: {E.EntryClass.LineNumber} {E.EntryClass}");
      }
    }
  }
