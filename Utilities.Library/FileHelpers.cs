using Logging.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;


namespace Utilities.Library
  {
  public class FileHelpers
    {
    // https://stackoverflow.com/questions/18996330/copying-files-and-subdirectories-to-another-directory-with-existing-files
    public static String CopyDir(String FromFolder, String ToFolder, Boolean Overwrite = false)
      {
      try
        {
        Directory
          .EnumerateFiles(FromFolder, "*.*", SearchOption.AllDirectories)
          .AsParallel()
          .ForAll(From =>
            {
              var To = From.Replace(FromFolder, ToFolder);
              // Create directories if needed
              var ToSubFolder = Path.GetDirectoryName(To);
              if (!String.IsNullOrWhiteSpace(ToSubFolder))
                {
                Directory.CreateDirectory(ToSubFolder);
                }

              File.Copy(From, To, Overwrite);
            });
        return String.Empty;
        }

      catch (Exception E)
        {
        return Log.Trace("Failed to copy directories because " + E.Message, LogEventType.Error);
        }
      }

    public static string MoveDir(String FromFolder, String ToFolder, Boolean Overwrite = false)
      {
      try
        {
        Directory
         .EnumerateFiles(FromFolder, "*", SearchOption.AllDirectories)
         .AsParallel()
         .ForAll(From =>
         {
           var To = From.Replace(FromFolder, ToFolder);
           // Create directories if needed
           var ToSubFolder = Path.GetDirectoryName(To);
           if (!String.IsNullOrWhiteSpace(ToSubFolder))
             {
             Directory.CreateDirectory(ToSubFolder);
             }
           File.Move(From, To, Overwrite);
         }
         );
        return String.Empty;
        }
      catch (Exception E)
        {
        return Log.Trace("Failed to copy directories because " + E.Message, LogEventType.Error);
        }
      }

    [SupportedOSPlatform("windows")]
    public static void EmptyDirectory(String Directory, bool filesOnly = false)
      {
      // First delete all the files, making sure they are not readonly
      var StackA = new Stack<DirectoryInfo>();
      StackA.Push(new DirectoryInfo(Directory));

      var StackB = new Stack<DirectoryInfo>();
      while (StackA.Any())
        {
        var Dir = StackA.Pop();
        foreach (var File in Dir.GetFiles())
          {
          File.IsReadOnly = false;
          File.Delete();
          }

        foreach (var SubDir in Dir.GetDirectories())
          {
          SetPermissions(SubDir.FullName);
          StackA.Push(SubDir);
          StackB.Push(SubDir);
          }
        if (!filesOnly)
          {
          System.IO.Directory.Delete(Directory, false);
          }
        }

      // Then delete the sub directories depth first
      while (StackB.Any())
        {
        StackB.Pop().Delete();
        }
      }

    [SupportedOSPlatform("windows")]
    private static void SetPermissions(String DirPath)
      {
      var Info = new DirectoryInfo(DirPath);
      var Self = WindowsIdentity.GetCurrent();
      var Ds = Info.GetAccessControl();
      Ds.AddAccessRule(new FileSystemAccessRule(Self.Name,
        FileSystemRights.FullControl,
        InheritanceFlags.ObjectInherit |
        InheritanceFlags.ContainerInherit,
        PropagationFlags.None,
        AccessControlType.Allow));
      Info.SetAccessControl(Ds);
      }

    // Safe way to delete a single file
    public static String DeleteSingleFile(String FilePath)
      {
      if (File.Exists(FilePath))
        {
        // Use a try block to catch IOExceptions, to
        // handle the case of the file already being
        // opened by another process.
        try
          {
          File.Delete(FilePath);
          return String.Empty;
          }
        catch (IOException E)
          {
          Console.WriteLine(E.Message);
          return Log.Trace("Cannot delete" + FilePath + "because " + E.Message, LogEventType.Message);
          }
        }
      return String.Empty;
      }

    public static void CreateEmptyFile(String Filename)
      {
      File.Create(Filename).Dispose();
      }

    public static bool RenameFileWithSingleQuotes(string filePath)
      {
      if (!File.Exists(filePath))
        {
        return false;
        }
      var newPath = filePath.Replace("\'", "");
      if (newPath.Equals(filePath))
        {
        return true;
        }
      try
        {
        File.Move(filePath, newPath);
        return true;
        }
      catch (Exception ex)
        {
        Log.Trace($"Cannot remove singe quotes in file path {filePath}", ex, LogEventType.Error);
        return false;
        }
      }

    // https://stackoverflow.com/questions/3855956/check-if-an-executable-exists-in-the-windows-path
    // Get the full path, using an environment search
    public static string GetFullPath(string fileName)
      {
      if (File.Exists(fileName))
        return Path.GetFullPath(fileName);

      var values = Environment.GetEnvironmentVariable("PATH");
      foreach (var path in values.Split(Path.PathSeparator))
        {
        var fullPath = Path.Combine(path, fileName);
        if (File.Exists(fullPath))
          return fullPath;
        }
      return null;
      }
    }
  }
