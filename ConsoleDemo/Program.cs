using System;

namespace ConsoleDemo
  {
  class Program
    {
    static void Main(string[] args)
      {
      Console.WriteLine("SQLite database Demo");
      DataAccessDemo.SQLiteDatabaseDemo();
      Console.ReadLine();

      Console.WriteLine("TreeBuilder Demo");
      TreeBuilderDemo.TreeDemo();
      Console.ReadLine();

      Console.WriteLine("SevenZip Demo");
      SevenZipDemo.RunSevenZipDemo();
      Console.ReadLine();
      }
    }
  }
