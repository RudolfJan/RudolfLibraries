using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Styles.Library.Models;

namespace Styles.Library.Helpers
  {
  public class FileIOHelpers
    {
    public static string GetSaveFileName(SaveFileModel saveFileParams)
      {
      SaveFileDialog dialog = new SaveFileDialog
        {
        CheckFileExists = saveFileParams.CheckFileExists,
        CheckPathExists = saveFileParams.CheckPathExists,
        CreatePrompt = saveFileParams.CreatePrompt,
        CustomPlaces = saveFileParams.CustomPlaces,
        DefaultExt = saveFileParams.DefaultExt,
        DereferenceLinks = saveFileParams.DereferenceLinks,
        FileName = saveFileParams.FileName,
        // FileNames not supported!
        Filter = saveFileParams.Filter,
        FilterIndex = saveFileParams.FilterIndex,
        InitialDirectory = saveFileParams.InitialDirectory,
        OverwritePrompt = saveFileParams.OverWriteprompt,
        Title = saveFileParams.Title
        };
      if (dialog.ShowDialog() == true)
        {
        saveFileParams.FileName = dialog.FileName;
        saveFileParams.SafeFileName = dialog.SafeFileName;
        saveFileParams.SafeFileNames = dialog.SafeFileNames;
        return dialog.FileName;
        }
      return "";
      }

    public static string GetOpenFileName(OpenFileModel openFileParams)
      {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.CheckFileExists = openFileParams.CheckFileExists;
      dialog.CheckPathExists = openFileParams.CheckPathExists;
      dialog.CustomPlaces = openFileParams.CustomPlaces;
      dialog.DefaultExt = openFileParams.DefaultExt;
      dialog.DereferenceLinks = openFileParams.DereferenceLinks;
      dialog.FileName = openFileParams.FileName;
      // FileNames not supported!
      dialog.Filter = openFileParams.Filter;
      dialog.FilterIndex = openFileParams.FilterIndex;
      dialog.InitialDirectory = openFileParams.InitialDirectory;
      dialog.Title = openFileParams.Title;
      if (dialog.ShowDialog() == true)
        {
        openFileParams.FileName = dialog.FileName;
        openFileParams.SafeFileName = dialog.SafeFileName;
        openFileParams.SafeFileNames = dialog.SafeFileNames;
        return dialog.FileName;
        }
      return "";
      }

    }
  }
