using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Library.TreeBuilders;


namespace ConsoleDemo
  {
  public class TreeBuilderDemo
    {

    public static void TreeDemo()
      {
      string TestDirectoryPath1 = @"C:\RW_Tools"; // Eventually replace this with a suitable directory
      var result1 = FileTreeBuilder.BuildTree(TestDirectoryPath1);

      string TestDirectoryPath2 = ".";
      var result2 = FileTreeBuilder.BuildTree(TestDirectoryPath2);

      ReportDir(result2, false, false);
      Console.WriteLine();
      Console.ReadLine();
      ReportDir(result2, false, true);
      Console.WriteLine();
      Console.ReadLine();
      ReportDir(result2, true, false);
      Console.WriteLine();
      Console.ReadLine();
      FileTreeBuilder.SetSelected(result2.DirNodeList[0], false);
      FileTreeBuilder.SetSelected(result2.DirNodeList[1], false);

      ReportDir(result2, true, false);
      string nonExistentPath = "C:\\Temp\\DoesNotExist";
      var result3 = FileTreeBuilder.BuildTree(nonExistentPath);
      }

    private static void ReportDir(TreeNodeModel tree, bool selectedOnly, bool filesOnly)
      {
      var list = FileTreeBuilder.TreeToStringList(tree , selectedOnly, filesOnly);
      foreach (var item in list)
        {
        Console.WriteLine(item);
        }
      }
    }
  }
