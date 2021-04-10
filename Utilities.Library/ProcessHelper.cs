using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
    }
  }
