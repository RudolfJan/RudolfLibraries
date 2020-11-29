using System.Collections.Generic;
using System.IO;

namespace Utilities.Library.TreeBuilders
	{
	public class TreeItemProvider
		{
		internal List<FileEntryModel> GetDirAndFileItems(string Path, bool Always = false)
			{
			var Items = new List<FileEntryModel>();

			var DirInfo = new DirectoryInfo(Path);
      foreach (var Directory in DirInfo.GetDirectories())
        {
        var DirItem = new DirectoryItem
          {
          Name = Directory.Name,
          Path = Directory.FullName,
          DirectoryItems = GetDirAndFileItems(Directory.FullName, Always)
          };
        Items.Add(DirItem);
        }

			foreach (var File in DirInfo.GetFiles())
				{
				var Item = new FileItem
					{
					Name = File.Name,
					Path = File.FullName,
					};
				Items.Add(Item);
				}
			return Items;
			}

		// Will only return directories 
		internal List<FileEntryModel> GetDirItems(string Path)
			{
			var Items = new List<FileEntryModel>();

			var DirInfo = new DirectoryInfo(Path);
      foreach (var Directory in DirInfo.GetDirectories())
				{
        var DirItem = new DirectoryItem
          {
          Name = Directory.Name,
          Path = Directory.FullName,
          DirectoryItems = GetDirItems(Directory.FullName)
          };
        Items.Add(DirItem);
				}
			return Items;
			}
		}
  }
