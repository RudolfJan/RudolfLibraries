using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Utilities.Library.TreeBuilders
	{
	public class DirectoryItem : FileEntryModel
		{
		public List<FileEntryModel> DirectoryItems { get; set; } =
			new List<FileEntryModel>();

		public DirectoryInfo DirectoryDetails { get; set; }
		}
  }
