using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utilities.Library
  {
  public static class LocateProgram

    {
    public static string FindFile(string exeFileName)
      {
      // try 64 bit program directory first
      string output="";
      if(!string.IsNullOrEmpty(output=FindFile(exeFileName,Environment.SpecialFolder.ProgramFiles)))
        {
        return output;
        }
        
      if (!string.IsNullOrEmpty(output = FindFile(exeFileName, Environment.SpecialFolder.ProgramFilesX86)))
        {
        return output;
        }
      if (!string.IsNullOrEmpty(output = FindFile(exeFileName, Environment.SpecialFolder.CommonProgramFiles)))
        {
        return output;
        }

      if (!string.IsNullOrEmpty(output = FindFile(exeFileName, Environment.SpecialFolder.CommonProgramFilesX86)))
        {
        return output;
        }
      return "";
      }


    public static string FindFile(string exeFileName, System.Environment.SpecialFolder folder)
      {
      var folderPath = Environment.GetFolderPath(folder);
      var path = $"{folderPath}{exeFileName}";
        if (File.Exists(path))
          {
          return path;
          }
        return "";
        }
      }
  }

