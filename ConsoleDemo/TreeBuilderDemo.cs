using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Library.TreeBuilders;


namespace ConsoleDemo
  {
  public class TreeBuilderDemo
    {
    public static string TestDirectoryPath { get; set; }

    public static void TreeDemo()
      {
      TestDirectoryPath = @"C:\RW_Tools"; // Eventually replace this with a suitable directory
      var tree = TreeBuilder.BuildTree(TestDirectoryPath);

      foreach (var file in tree.TreeItems)
        {
        Console.WriteLine($"{file.Path}");
        if (file is DirectoryItem) // TODO make a complete tree walker, to create a list of all files and/or all directories, for now only two levels.
          {
          foreach (var subfile in ((DirectoryItem) file).DirectoryItems)
            {
            Console.WriteLine($"{subfile.Path}");
            }
          }
        }
      }

    }
  }
