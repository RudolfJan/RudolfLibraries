using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Library.FileCollector
  {
  public class FileCollector
    {
    public static List<string> CollectFiles(string directory, string searchPattern, bool includeSubdirectories)
      {
      var files = new List<string>();
      var searchOption = includeSubdirectories ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly;
      try
        {
        files.AddRange(System.IO.Directory.GetFiles(directory, searchPattern, searchOption));
        }
      catch (Exception ex)
        {
        Console.WriteLine($"Error collecting files: {ex.Message}");
        }
      return files;
      }
    }
  }
