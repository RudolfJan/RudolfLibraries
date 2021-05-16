using Logging.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Utilities.Library.TextHelpers;

namespace Utilities.Library.Zip
  {
  public class SevenZipLib
    {
		private static string _sevenZipProgram =null;


		// Setup the Path, you need to do this in a separate step, but only once
    public static void InitZip(string sevenZipProgram)
      {
      if (File.Exists(sevenZipProgram))
        {
        _sevenZipProgram = sevenZipProgram;
        }
			else
        {
        Log.Trace($"SevenZip program path not found {sevenZipProgram}", LogEventType.Error);
        throw new FileNotFoundException(sevenZipProgram);
        }
      }

    public static string GetSevenZipProgram()
      {
      if (_sevenZipProgram == null)
        {
        Log.Trace($"SevenZip program path not set {_sevenZipProgram}", LogEventType.Error);
        throw new ArgumentNullException();
        }
      return _sevenZipProgram;
      }

    public static string GetSevenZipWindowsProgram()
      {
      var path = Path.GetDirectoryName(_sevenZipProgram);
      var sevenZipWindowsProgram = $"{path}\\7zFm.exe";
      if (sevenZipWindowsProgram == null)
        {
        Log.Trace($"SevenZip program path not set {sevenZipWindowsProgram}", LogEventType.Error);
        throw new ArgumentNullException();
        }
      return sevenZipWindowsProgram;
      }



    private static string RunConsole(string arguments)
      {
      using (var MyProcess = new Process())
        {
        try
          {
          return ProcessHelper.RunProcess(GetSevenZipProgram(),arguments,true,true,WindowStyle: ProcessWindowStyle.Hidden,
            RedirectStandardOutput:true, RedirectStandardError:true,CreateNoWindow:true,UseShellExecute:false);
            }
        catch (Exception ex)
          {
          Log.Trace("SevenZip error", ex, LogEventType.Error);
          }

        return string.Empty;
        }
      }

    public static string OpenZipFile(string archive)
      {
      if (File.Exists(archive))
        {
        using (var MyProcess = new Process())
          {
          try
            {
            return ProcessHelper.RunProcess(GetSevenZipWindowsProgram(), TextHelper.QuoteFilename(archive),true, WindowStyle: ProcessWindowStyle.Maximized);
            }
          catch (Exception ex)
            {
            return Log.Trace($"Cannot open zip file {archive} using 7Zip", ex, LogEventType.Error);
            }
          }
        }
      return string.Empty;
      }

    // Extract all files, take filepath into account
    public static String ExtractAll(String Archive, String OutputDirectory, String Filter,
			Boolean Recursive = false)
			{
			var RecursiveOption = string.Empty;
			if (Recursive)
				{
				RecursiveOption = " -r";
				}
      var arguments =
        "-y x \"" + Archive + "\" -o\"" + OutputDirectory + "\" " + Filter + RecursiveOption;
			if (File.Exists(Archive))
        {
        return RunConsole(arguments);
        }
      return string.Empty;
      }

		// Add all files from a directory to a zip archive, optionally do this recursively, input directory must end with backslash!
		public static String AddDirectory(String Archive, String InputDirectory, Boolean Recursive = false)
      {
      string recursion=string.Empty;
      string excludeSubfolders = " -x!*\\";
      if (Recursive)
        {
        recursion = "-r ";
        excludeSubfolders = "";
        }
      InputDirectory = $"{TextHelper.AddBackslash(InputDirectory)}*";
      var arguments = $"a {TextHelper.QuoteFilename(Archive)} {recursion}{TextHelper.QuoteFilename(InputDirectory)}*{excludeSubfolders}";
      return RunConsole(arguments);
      }

    // Adds comma separated InputFiles to an archive
    public static string AddFiles(string archive, string inputFiles)
      {
      string[] tmp = inputFiles.Split(',');
      List<string> filesList = new List<string>(tmp);
      return AddFiles(archive, filesList);
      }

    // Add files in a list to to a .7z archive
    // 7z.exe a test.zip C:\directory\* -x!*/ -tzip
    public static string AddFiles(string archive, List<string> inputFiles)
			{
      string inputForSevenZip = "";
      foreach (var filePath in inputFiles)
        {
        if (inputForSevenZip.Length > 0)
          {

          inputForSevenZip += " ";
          }
        inputForSevenZip += TextHelper.QuoteFilename(filePath);
        }
      var arguments = $"a {TextHelper.QuoteFilename(archive)} {inputForSevenZip}";
      return RunConsole(arguments);
      }

		// Extract a single file, take filepath into account
    public static String ExtractSingle(String Archive, String OutputDirectory, String FullName)
      {
      var arguments = "-y x \"" + Archive + "\" -o\"" + OutputDirectory + "\" \"" + FullName + "\"";
      if (File.Exists(Archive))
        {
        return RunConsole(arguments);
        }
			return string.Empty;
      }

    // This extraction will NOT take into account the filepath
    // used to be named GetZipFile, but the new name is more generic.
    public static String ExtractSingleWithoutPath(String Archive, String ArchiveEntry, String OutputDirectory)
      {

      if (File.Exists(Archive))
        {
        var arguments =
          "-y e \"" + Archive + "\" -o\"" + OutputDirectory + "\" \"" + ArchiveEntry + "\"";
        return RunConsole(arguments);
        }

      return String.Empty;
      }


    // Extract all files from archive, take filepath into account
    public static String ExtractAll(String Archive, String OutputDirectory)
      {
      var arguments = "-y -r -aoa x \"" + Archive + "\" -o\"" + OutputDirectory + "\" *";
      if (File.Exists(Archive))
        {
        return RunConsole(arguments);
        }
			return string.Empty;
      }

    // Extract all files at a given directory path. Note: you need to specify the wildcard pattern as well, e.g. * for all files
		public static String ExtractDirectory(String Archive, String InputDirectory, String OutputDirectory)
			{
			var arguments = "-y -r -aoa x \"" + Archive + "\" -o\"" + OutputDirectory + "\" " + InputDirectory;
      if (File.Exists(Archive))
        {
        return RunConsole(arguments);
        }
      return string.Empty;
      }


    // suppressHeaders will suppress a lot of stuff you do not want to see if you need to process this info. It is not set as default, for backward compatibility reasons.
    // The preferred value for new applications is true of course.
    // Renamed, used to be called ListZipFiles
		public static void ListFilesInArchive(string archive, out string result, bool suppressHeaders=false)
			{
			result = string.Empty;
      var suppressHeadersSwitch = string.Empty;
      if (suppressHeaders)
        {
        suppressHeadersSwitch = " -ba";
        }
      var arguments = $"l -r{suppressHeadersSwitch} \"{archive}";
      if (File.Exists(archive))
        {
        result = RunConsole(arguments);
        }
			}
		}
	}
