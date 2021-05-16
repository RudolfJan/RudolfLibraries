using System;
using System.IO;
using Utilities.Library.Zip;

namespace ConsoleDemo
  {
  public class SevenZipDemo
    {

    public static void  RunSevenZipDemo()
      {
      string SevenZipFilePath = @"C:\Program Files\7-Zip\7z.exe";
      SevenZipLib.InitZip(SevenZipFilePath);
      // We need a demo archive. For this purpose we can use the current directory
      string path = System.AppContext.BaseDirectory;
      string demoArchiveName = $"{path}SevenZipDemoArchive.7z";
      string singleFileDemoArchive = $"{path}SingleFileDemoArchive.7z";
      string multipleFileDemoArchive = $"{path}MultipleFileDemoArchive.7z";
      // Set initial demo state, by deleting the demo Archive if it exists
      DeleteArchive(demoArchiveName);
      DeleteArchive(singleFileDemoArchive);
      DeleteArchive(multipleFileDemoArchive);


      string report;
      if (!CreateArchiveForDirectory(demoArchiveName, path)) return;

      string inputFile, inputFiles;
      // Usage note: if you start the path with .\\ it will NOT include the relative file path!
      inputFile = @"SQLiteTest\SQL\PersonsTable.sql";
      inputFiles = @"runtimes\win-x64\native\SQLite.Interop.dll,runtimes\win-x86\native\SQLite.Interop.dll";
      AddFilesToArchive(singleFileDemoArchive, inputFile);
      AddFilesToArchive(singleFileDemoArchive, inputFile);
      // Now a combined test
      AddFilesToArchive(demoArchiveName, inputFile);
      AddFilesToArchive(demoArchiveName, inputFiles);

      // Test file extraction
      string outputFolder = "C:\\Temp\\ArchiveTest\\";

      report = ExtractFilesTest(demoArchiveName, inputFile, outputFolder);

      // Same test, but now we do NOT use the path in the output
      ExtractWithoutPath(demoArchiveName, inputFile, outputFolder);

      var testDirectory = $"{outputFolder}Dirtest";
      if (Directory.Exists(testDirectory))
        {
        Directory.Delete(testDirectory, true);
        }

      // test extract a directory in the archive

      TestExtractDirectory(demoArchiveName, testDirectory);

      // test extract all files in archive
      SevenZipLib.ExtractAll(demoArchiveName, testDirectory);

      // test extract all but filtered
      TestExtractFilteredFileFormArchive(demoArchiveName, outputFolder);

      // Test list archive
      SevenZipLib.ListFilesInArchive(demoArchiveName, out report, true);
      Console.WriteLine(report);

      SevenZipLib.OpenZipFile(singleFileDemoArchive);
      SevenZipLib.OpenZipFile(multipleFileDemoArchive);
      SevenZipLib.OpenZipFile(demoArchiveName);
      }

    private static void TestExtractFilteredFileFormArchive(string demoArchiveName, string outputFolder)
      {
      var testDirectory2 = $"{outputFolder}Dirtest2";
      var checkPath = $"{testDirectory2}\\SQLiteTest\\SQL\\PersonsTable.sql";
      if (File.Exists(checkPath))
        {
        File.Delete(checkPath);
        }
      SevenZipLib.ExtractAll(demoArchiveName, testDirectory2, "*.sql", true);
      if (File.Exists(checkPath))
        {
        Console.WriteLine($"Passed test writing selected files\r\n");
        }
      else
        {
        Console.WriteLine($"FAILED test writing selected files\r\n");
        }
      }

    private static string TestExtractDirectory(string demoArchiveName, string testDirectory)
      {
      var report = SevenZipLib.ExtractDirectory(demoArchiveName, "SQLiteTest", testDirectory);
      Console.WriteLine(report);
      return report;
      }

    private static string ExtractWithoutPath(string demoArchiveName, string inputFile, string outputFolder)
      {
      string report;
      var resultFile = Path.GetFileName(inputFile);
      report = SevenZipLib.ExtractSingleWithoutPath(demoArchiveName, inputFile, outputFolder);
      Console.WriteLine(report);

      if (File.Exists($"{outputFolder}{resultFile}"))
        {
        Console.WriteLine($"File {resultFile} is written to disk properly");
        }
      else
        {
        Console.WriteLine($"FAILED File {resultFile} is NOT written to disk properly");
        }

      return report;
      }

    private static string ExtractFilesTest(string demoArchiveName, string inputFile, string outputFolder)
      {
      string report;
      if (Directory.Exists($"{outputFolder}{inputFile}"))
        {
        Directory.Delete($"{outputFolder}{inputFile}", true);
        }

      // Extract single, using file path in target folder

      report = SevenZipLib.ExtractSingle(demoArchiveName, outputFolder, inputFile);
      Console.WriteLine(report);
      if (File.Exists($"{outputFolder}{inputFile}"))
        {
        Console.WriteLine($"File {inputFile} is written to disk properly");
        }
      else
        {
        Console.WriteLine($"FAILED File {inputFile} is NOT written to disk properly");
        }

      return report;
      }

    private static void AddFilesToArchive(string archive, string inputFiles)
      {
      var report = SevenZipLib.AddFiles(archive, inputFiles);
      Console.WriteLine(report);
      }

    private static bool CreateArchiveForDirectory(string demoArchiveName, string path)
      {
      var report = SevenZipLib.AddDirectory(demoArchiveName, path, false);
      Console.WriteLine(report);

      if (File.Exists(demoArchiveName))
        {
        Console.WriteLine($"Created archive {demoArchiveName}");
        }
      else
        {
        //TODO we may consider to replace this by assertions
        Console.WriteLine($"FAILED to created archive {demoArchiveName}");
        Console.ReadLine();
        return false;
        }
      return true;
      }

    private static void DeleteArchive(string demoArchiveName)
      {
      if (File.Exists(demoArchiveName))
        {
        File.Delete(demoArchiveName);
        }
      }
    }
  }
