using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Text;
using System.Windows.Data;

namespace TreeBuilders.Library.Wpf
  {
  public class TreeNodeModel
    {
    public DirectoryInfo Root { get; set; }
    public List<FileNodeModel> FileNodeList { get; set; } = new List<FileNodeModel>();
    public List<TreeNodeModel> DirNodeList { get; set; } = new List<TreeNodeModel>();
    public bool IsSelected { get; set; } = true;
    public IList TreeViewFileTree
      {
      get
        {
        return new CompositeCollection()
           {
           new CollectionContainer() { Collection = FileNodeList },
           new CollectionContainer() { Collection = DirNodeList }
           };
        }
      }
    }
  }
