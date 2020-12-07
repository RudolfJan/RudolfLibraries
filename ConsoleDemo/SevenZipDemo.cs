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
      SevenZip.InitZip(SevenZipFilePath);
      // We need a demo archive. For this purpose we can use the current directory
      string path = System.AppContext.BaseDirectory;
      string demoArchiveName = $"{path}SevenZipDemoArchive.7z";
      string singleFileDemoArchive = $"{path}SingleFileDemoArchive.7z";
      string multipleFileDemoArchive = $"{path}MultipleFileDemoArchive.7z";
      // Set initial demo state, by deleting the demo Archive if it exists
      DeleteArchive(demoArchiveName);
      DeleteArchive(singleFileDemoArchive);
      DeleteArchive(multipleFileDemoArchive);
      var report = SevenZip.AddDirectory(demoArchiveName, path, false);
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
        return;
        }

      // Usage note: if you start the path with .\\ it will NOT include the relative file path!
      string inputFile = @"SQLiteTest\SQL\PersonsTable.sql";
      report = SevenZip.AddFiles(singleFileDemoArchive, inputFile);
      Console.WriteLine(report);
      SevenZip.OpenZipFile(singleFileDemoArchive);
      var inputFiles =
        @"runtimes\win-x64\native\SQLite.Interop.dll,runtimes\win-x86\native\SQLite.Interop.dll";
      report = SevenZip.AddFiles(multipleFileDemoArchive, inputFiles);
      Console.WriteLine(report);
      SevenZip.OpenZipFile(multipleFileDemoArchive);

      // Now a combined test
      report = SevenZip.AddFiles(demoArchiveName, inputFile);
      Console.WriteLine(report);
      report = SevenZip.AddFiles(demoArchiveName, inputFiles);
      Console.WriteLine(report);
      SevenZip.OpenZipFile(demoArchiveName);

      // Test file extraction
      string outputFolder="C:\\Temp\\ArchiveTest\\";

      if (Directory.Exists($"{outputFolder}{inputFile}"))
        {
        Directory.Delete($"{outputFolder}{inputFile}",true);
        }

      // Extract single, using file path in target folder

      report= SevenZip.ExtractSingle(demoArchiveName, outputFolder, inputFile);
      Console.WriteLine(report);
      if (File.Exists($"{outputFolder}{inputFile}"))
        {
        Console.WriteLine($"File {inputFile} is written to disk properly");
        }
      else
        {
        Console.WriteLine($"FAILED File {inputFile} is NOT written to disk properly");
        }

      // Same test, but now we do NOT use the path in the output
      var resultFile = Path.GetFileName(inputFile);
      report = SevenZip.ExtractSingleWithoutPath(demoArchiveName, inputFile,outputFolder);
      Console.WriteLine(report);
      
      if (File.Exists($"{outputFolder}{resultFile}"))
        {
        Console.WriteLine($"File {resultFile} is written to disk properly");
        }
      else
        {
        Console.WriteLine($"FAILED File {resultFile} is NOT written to disk properly");
        }

      var testDirectory = $"{outputFolder}Dirtest";
      if (Directory.Exists(testDirectory))
        {
        Directory.Delete(testDirectory,true);
        }

      // test extract a directory in the archive

      report=SevenZip.ExtractDirectory(demoArchiveName,"SQLiteTest", testDirectory);
      Console.WriteLine(report);

      // test extract all files in archive
      report = SevenZip.ExtractAll(demoArchiveName, testDirectory);

      // Test list archive

      SevenZip.ListFilesInArchive(demoArchiveName, out report, true);
      Console.WriteLine(report);
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
