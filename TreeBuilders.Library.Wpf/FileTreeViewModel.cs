﻿using Logging.Library;
using Styles.Library.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Documents;
using Utilities.Library;

namespace TreeBuilders.Library.Wpf
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


		public FileTreeViewModel(string _rootFolder, bool onlyDirectories=false)
			{
			RootFolder= _rootFolder;
			if(!Directory.Exists(RootFolder))
        {
				Log.Trace($"Root folder cannot be found {RootFolder}");
        }
			Init();
			}

		private void Init()
      {
			FileTree = FileTreeBuilder.BuildTree(RootFolder);
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
