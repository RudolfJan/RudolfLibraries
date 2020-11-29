using System.Collections.Generic;

namespace Utilities.Library.TreeBuilders
  {
  public class FileTreeModel
    {
    public List<FileEntryModel> TreeItems { get; set; }
    public TreeItemProvider FileTree { get; set; }
    }
  }
