using Caliburn.Micro;
using Logging.Library;
using Styles.Library.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Utilities.Library;

namespace TreeBuilders.Library.Wpf.ViewModels
  {
 public  class FileTreeViewModel: Screen
    {
		public string RootFolder { get;set; } 


		public string FileImage { get;set; }

		public string FolderImage { get;set; }
		public BitmapImage FolderBitMap { get; set; }
		public BitmapImage FileBitMap { get; set; }

		private TreeNodeModel _FileTree;
		public TreeNodeModel FileTree
			{
			get { return _FileTree; }
			set
				{
				_FileTree = value;
				NotifyOfPropertyChange("FileTree");
				}
			}

		private FileNodeModel _SelectedFileNode;
		public FileNodeModel SelectedFileNode
			{
			get { return _SelectedFileNode; }
			set
				{
				_SelectedFileNode = value;
				NotifyOfPropertyChange("SelectedFileNode");
				}
			}

		private TreeNodeModel _SelectedTreeNode;
		public TreeNodeModel SelectedTreeNode
			{
			get { return _SelectedTreeNode; }
			set
				{
				_SelectedTreeNode = value;
				NotifyOfPropertyChange("SelectedTreeNode");
				}
			}


		public FileTreeViewModel()
			{
			if(!Directory.Exists(RootFolder))
        {
				Log.Trace($"Root folder cannot be found {RootFolder}");
        }
			Init();
			}

		private void Init()
      {
			FileTree = FileTreeBuilder.BuildTree(RootFolder);
			SetImages();
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
			catch (Exception ex)
				{
				Log.Trace("Cannot retrieve images for TreeViewer ", ex, LogEventType.Error);
				}
			}

		internal void OpenDocument()
      {
			if(SelectedFileNode!=null)
				{
        ProcessHelper.OpenGenericFile(SelectedFileNode.FileEntry.FullName);
				}
      }

    internal void OpenFolder()
      {
			if(SelectedTreeNode!=null)
				{
        ProcessHelper.OpenFolder(SelectedTreeNode.Root.FullName);
				}
      }
    }
	}
