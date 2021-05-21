using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TreeBuilders.Library.Wpf;

namespace WPFDemo
  {
 
  public partial class FileTreeDemoView : Window
    {
    public FileTreeViewModel Tree {get;set ;}
  public FileTreeDemoView()
      {
      InitializeComponent();
      FileTreeViewControl.FolderImage = "Images\\folder.png";
      FileTreeViewControl.FileImage = "Images\\file_extension_doc.png";
      FileTreeViewControl.SetImages();
      Tree = new FileTreeViewModel("C:\\Temp\\");
      FileTreeViewControl.Tree=Tree;
      FileTreeViewControl.DataContext=Tree;
      }
    }
  }
