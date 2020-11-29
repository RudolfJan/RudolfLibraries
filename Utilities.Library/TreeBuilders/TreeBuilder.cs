﻿using System.IO;

namespace Utilities.Library.TreeBuilders
  {
  public class TreeBuilder
    {
    public FileTreeModel Tree { get; set; } =new FileTreeModel();

    public static FileTreeModel BuildTree(DirectoryInfo dir)
      {
      var Tree = new FileTreeModel
        {
        FileTree = new TreeItemProvider()
        };
      Tree.TreeItems = Tree.FileTree.GetDirAndFileItems(dir.FullName);
      return Tree;
      }
    public static FileTreeModel BuildTree(string folderPath)
      {
      var dir = new DirectoryInfo(folderPath);
      return BuildTree(dir);
      }
    }
  }
