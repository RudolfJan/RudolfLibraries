using Logging.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Utilities.Library.TextHelpers;

namespace Utilities.Library
  {
  public class ProcessHelper
    {
    // call this function if a process needs to be run
    public static String RunProcess(
        String Filepath,
        String Arguments = "",
        bool WaitForExit = false,  // if true only returns once process has exited, otherwise behaves like fire-and-forget
        bool ContinuousOutput = false,  // specify which output formatting should be used. I couldn't think of a better name for the moment
        ProcessWindowStyle WindowStyle = ProcessWindowStyle.Normal,  // only override if windowStyle is not normal
        bool CreateNoWindow = false,  // some used process arguments. Should be self-explanatory
        bool UseShellExecute = false,
        bool RedirectStandardOutput = false,
        bool RedirectStandardError = false)
      {
      Filepath= FileHelpers.GetFullPath(Filepath); // retrieve the path for the file,
      if(!File.Exists(Filepath))
        {
        Log.Trace($"Cannot execute program {Filepath} because it does not exist",LogEventType.Error);
        return string.Empty;
        }
      using (var GenericProcess = new Process())
        {
        GenericProcess.StartInfo.FileName = Filepath;
        GenericProcess.StartInfo.Arguments = Arguments;
        GenericProcess.StartInfo.WindowStyle = WindowStyle;
        GenericProcess.StartInfo.CreateNoWindow = CreateNoWindow;
        GenericProcess.StartInfo.UseShellExecute = UseShellExecute;
        GenericProcess.StartInfo.RedirectStandardOutput = RedirectStandardOutput;
        GenericProcess.StartInfo.RedirectStandardError = RedirectStandardError;
        GenericProcess.Start();

        if (WaitForExit)
          if (!ContinuousOutput)
            {
            var ProcessReturn = "";
            while (GenericProcess.StandardOutput.EndOfStream)
              {
              ProcessReturn += GenericProcess.StandardOutput.ReadLine() + "\r\n";
              }
            GenericProcess.WaitForExit();
            ProcessReturn += GenericProcess.StandardOutput.ReadToEnd();
            return ProcessReturn;
            }
          else
            {
            var ProcessReturn = GenericProcess.StandardOutput.ReadToEnd();
            ProcessReturn += GenericProcess.StandardError.ReadToEnd();
            GenericProcess.WaitForExit();
            return ProcessReturn;
            }
        }
      return String.Empty;
      }

    public static String OpenGenericFile(String Filepath)
      {
      try
        {
        if (Filepath.Contains("\'"))
          {
          //TODO fix this work around
          return Log.Trace("Cannot open file " + Filepath + " because it contains single quotes. Remove the quote from the file path", LogEventType.Error);
          }

        if (File.Exists(Filepath))
          {
          RunProcess("explorer.exe", TextHelper.QuoteFilename(Filepath), WindowStyle: ProcessWindowStyle.Maximized);
          return String.Empty;
          }
        }
      catch (Exception E)
        {
        return Log.Trace("Cannot open file " + Filepath + " reason: " + E.Message, LogEventType.Error);
        }

      return Log.Trace("Cannot find file " + Filepath + " \r\nMake sure to install it at the correct location");
      }

    // Open folder from the application
    public static String OpenFolder(String FolderPath)
      {
      if (!Directory.Exists(FolderPath))
        {
        return "Directory does not exist " + FolderPath;
        }

      try
        {
        RunProcess("explorer.exe", TextHelper.QuoteFilename(FolderPath), WindowStyle: ProcessWindowStyle.Maximized);
        return String.Empty;
        }
      catch (Exception E)
        {
        return Log.Trace("Cannot open directory " + FolderPath + " reason: " + E.Message, LogEventType.Error);
        }
      }


    }
  }
