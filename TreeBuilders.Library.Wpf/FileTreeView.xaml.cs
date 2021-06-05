using Logging.Library;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TreeBuilders.Library.Wpf
  {
  public partial class FileTreeView : UserControl
    {
    public FileTreeViewModel Tree { get; set; }
    public string FolderImage { get;set; }
    public string FileImage { get; set; }
    public BitmapImage FolderBitMap { get;set; }
    public BitmapImage FileBitMap { get; set; }

    public FileTreeView()
      {
      InitializeComponent();
      FileTreeTreeView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(FileTreeTreeView_SelectedItemChanged);
      SetControlStates();
      }

    private void SetControlStates()
      {
      OpenFolderButton.IsEnabled= Tree?.SelectedTreeNode!=null;
      OpenDocumentButton.IsEnabled= Tree?.SelectedFileNode!=null;
      }

    public void Refresh()
      {
      FileTreeTreeView.Items.Refresh();
      }
    public void SetImages()
      {
      try
        {
      if (!string.IsNullOrEmpty(FolderImage))
        {
        // The part pack://siteoforigin:,,,/ sets the correct absolute path to the image.
        FolderBitMap = new BitmapImage(new Uri($"pack://siteoforigin:,,,/{FolderImage}", UriKind.RelativeOrAbsolute));
        }
      if (!string.IsNullOrEmpty(FileImage))
        {
        FileBitMap = new BitmapImage(new Uri($"pack://siteoforigin:,,,/{FileImage}", UriKind.RelativeOrAbsolute));
        }
        }
      catch(Exception ex)
        {
        Log.Trace("Cannot retrieve images for TreeViewer ",ex,LogEventType.Error);
        }
      }

    private void FileTreeTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
      {
      var selected = FileTreeTreeView.SelectedItem;
      if(selected is FileNodeModel)
        {
        Tree.SelectedFileNode=selected as FileNodeModel;
        Tree.SelectedTreeNode=null;
        SetControlStates();
        return;
        }
      if(selected is TreeNodeModel)
        {
        Tree.SelectedTreeNode=selected as TreeNodeModel;
        Tree.SelectedFileNode=null;
        }
      e.Handled = true;
      RoutedEventArgs Args = new RoutedEventArgs(NodeSelectionChangedEvent);
      RaiseEvent(Args);
      SetControlStates();
      }

    private void OpenDocumentButton_Click(object sender, System.Windows.RoutedEventArgs e)
      {
      Tree.OpenDocument();
      }

    private void OpenFolderButton_Click(object sender, System.Windows.RoutedEventArgs e)
      {
      Tree.OpenFolder();
      }


    public static readonly RoutedEvent NodeSelectionChangedEvent =
       EventManager.RegisterRoutedEvent("NodeSelectionChanged",
       RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(TreeView));

    public event RoutedEventHandler NodeSelectionChanged
      {
      add => AddHandler(NodeSelectionChangedEvent, value);
      remove => RemoveHandler(NodeSelectionChangedEvent, value);
      }
    }
  }
