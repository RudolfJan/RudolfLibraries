using Logging.Library;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities.Library;

namespace TreeBuilders.Library.Wpf
  {
  public class FileTreeBuilder
    {
    #region BuildTree

    public static TreeNodeModel BuildTree(string path, string fileFilter = "",
      string directoryFilter = "", bool onlyDirectories=false)
      {
      DirectoryInfo pathDirInfo = new DirectoryInfo(path);
      return BuildTree(pathDirInfo, fileFilter, directoryFilter, onlyDirectories);
      }

    public static TreeNodeModel BuildTree(DirectoryInfo path, string fileFilter =
      "", string directoryFilter = "", bool onlyDirectories = false)
      {
      try
        {
        if (path.Exists)
          {
          var tree = new TreeNodeModel
            {
            Root = path
            };
          return BuildTreeNode(tree, fileFilter, directoryFilter, onlyDirectories);
          }

        Log.Trace($"Directory {path.FullName} does not exist", LogEventType.Error);
        return null;
        }
      catch (Exception ex)
        {
        Log.Trace($"Cannot create directory tree {path.FullName}", ex, LogEventType.Error);
        return null;
        }
      }

    private static TreeNodeModel BuildTreeNode(TreeNodeModel node, string fileFilter = "",
      string directoryFilter = "", bool onlyDirectories = false)
      {
      if(!onlyDirectories)
        {
        BuildFileList(node, fileFilter);
        }

      DirectoryInfo[] dirList = node.Root.GetDirectories(directoryFilter);
      foreach (var dir in dirList)
        {
        var subNode = new TreeNodeModel
          {
          Root = dir
          };
        node.DirNodeList.Add(subNode);
        BuildTreeNode(subNode, fileFilter, directoryFilter, onlyDirectories);
        }
      return node;
      }

    private static void BuildFileList(TreeNodeModel node, string fileFilter)
      {
      FileInfo[] fileList = node.Root.GetFiles(fileFilter);
      foreach (var file in fileList)
        {
        node.FileNodeList.Add(new FileNodeModel { FileEntry = file });
        }
      }

    #endregion

    #region RemoveQuotes

    public static void RenameFilesToUnquoted(TreeNodeModel tree)
      {
      foreach(var file in tree.FileNodeList)
        {
        FileHelpers.RenameFileWithSingleQuotes(file.FileEntry.FullName);
        file.FileEntry=new FileInfo(file.FileEntry.FullName);
        }
      foreach(var dir in tree.DirNodeList)
        {
        RenameFilesToUnquoted(dir);
        }
      }



    #endregion

    #region Reports

    // Some simple filtering
    // filesOnly will not mention directories in the list
    // selectedOnly will take the IsSelected field into account. If a directory is not selected, the path will not be followed further down.
    public static List<string> TreeToStringList(TreeNodeModel tree, bool selectedOnly, bool filesOnly)
      {
      var output = new List<string>(0);
      AddNodeToStringList(tree, output, selectedOnly, filesOnly);
      return output;
      }

    private static List<string> AddNodeToStringList(TreeNodeModel tree, List<string> treeList, bool selectedOnly, bool filesOnly)
      {
      if (!selectedOnly || tree.IsSelected)
        {
        if (!filesOnly)
          {
          treeList.Add(tree.Root.FullName);
          }

        foreach (var node in tree.FileNodeList)
          {
          if (!selectedOnly || node.IsSelected)
            {
            treeList.Add(node.FileEntry.FullName);
            }
          }

        foreach (var dir in tree.DirNodeList)
          {
          AddNodeToStringList(dir, treeList, selectedOnly, filesOnly);
          }
        }

      return treeList;
      }


    public static void SetSelected(TreeNodeModel tree, bool isSelected)
      {
      tree.IsSelected= isSelected;
      foreach (var file in tree.FileNodeList)
        {
        file.IsSelected= isSelected;
        }

      foreach (var dir in tree.DirNodeList)
        {
        SetSelected(dir, isSelected);
        }
      }
    #endregion
    }
  }
