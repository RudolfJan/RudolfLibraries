using Styles.Library.Helpers;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Data;

namespace TreeBuilders.Library.Wpf
  {
  public class TreeNodeModel: Notifier
    {
		private DirectoryInfo _Root;
		public DirectoryInfo Root
			{
			get { return _Root; }
			set
				{
				_Root = value;
				OnPropertyChanged("Root");
				}
			}

		private ObservableCollection<FileNodeModel> _FileNodeList = new ObservableCollection<FileNodeModel>();
		public ObservableCollection<FileNodeModel> FileNodeList
			{
			get { return _FileNodeList; }
			set
				{
				_FileNodeList = value;
				OnPropertyChanged("FileNodeList");
				}
			}

		private ObservableCollection<TreeNodeModel> _DirNodeList= new ObservableCollection<TreeNodeModel>();
		public ObservableCollection<TreeNodeModel> DirNodeList
			{
			get { return _DirNodeList; }
			set
				{
				_DirNodeList = value;
				OnPropertyChanged("DirNodeList");
				}
			}


		private bool _IsSelected;
		public bool IsSelected
			{
			get { return _IsSelected; }
			set
				{
				_IsSelected = value;
				OnPropertyChanged("IsSelected");
				}
			}

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
