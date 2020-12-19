using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utilities.Library.TreeBuilders
  {
  public class TreeNodeModel
    {
    public DirectoryInfo Root { get; set; }
    public List<FileNodeModel> FileNodeList { get; set; } = new List<FileNodeModel>();
    public List<TreeNodeModel> DirNodeList { get; set; } = new List<TreeNodeModel>();
    public bool IsSelected { get; set; } = true;
    }
  }
