
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
 public  class FileTreeViewModel: Notifier
    {
		public string RootFolder { get;set; } 
		private TreeNodeModel _FileTree;
		public TreeNodeModel FileTree
			{
			get { return _FileTree; }
			set
				{
				_FileTree = value;
				OnPropertyChanged("FileTree");
				}
			}

    private bool _onlyDirectories;

    public bool OnlyDirectories
			{ 
      get { return _onlyDirectories; }
      set { _onlyDirectories = value; 
				OnPropertyChanged("OnlyDirecties");
					}
      }

    private FileNodeModel _SelectedFileNode;
		public FileNodeModel SelectedFileNode
			{
			get { return _SelectedFileNode; }
			set
				{
				_SelectedFileNode = value;
				OnPropertyChanged("SelectedFileNode");
				}
			}

		private TreeNodeModel _SelectedTreeNode;
		public TreeNodeModel SelectedTreeNode
			{
			get { return _SelectedTreeNode; }
			set
				{
				_SelectedTreeNode = value;
				OnPropertyChanged("SelectedTreeNode");
				}
			}


		public FileTreeViewModel(string rootFolder, bool onlyDirectories=false)
			{
			RootFolder= rootFolder;
			OnlyDirectories= onlyDirectories;
			if(!Directory.Exists(RootFolder))
        {
				Log.Trace($"Root folder cannot be found {RootFolder}");
        }
			Init();
			}

		private void Init()
      {
			FileTree = FileTreeBuilder.BuildTree(RootFolder,"","",OnlyDirectories);
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
