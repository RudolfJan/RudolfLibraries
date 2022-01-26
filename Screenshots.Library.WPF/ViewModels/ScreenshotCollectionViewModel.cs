using Screenshots.Library.DataAccess;
using Screenshots.Library.Logic;
using Screenshots.Library.Models;
using Styles.Library.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Library.TextHelpers;


namespace Screenshots.Library.WPF.ViewModels
{
  public class ScreenshotCollectionViewModel : Notifier
  {
    private List<CollectionModel> _CollectionList;
    public List<CollectionModel> CollectionList
    {
      get { return _CollectionList; }
      set
      {
        _CollectionList = value;
        OnPropertyChanged("CollectionList");
      }
    }

    private CollectionModel _SelectedCollection;
    public CollectionModel SelectedCollection
    {
      get { return _SelectedCollection; }
      set
      {
        _SelectedCollection = value;
        OnPropertyChanged("SelectedCollection");
      }
    }

    private string _CollectionName = string.Empty;
    public string CollectionName
    {
      get { return _CollectionName; }
      set
      {
        _CollectionName = value;
        OnPropertyChanged("CollectionName");
      }
    }

    private string _CollectionPath = string.Empty;
    public string CollectionPath
    {
      get { return _CollectionPath; }
      set
      {
        _CollectionPath = value;
        OnPropertyChanged("CollectionPath");
      }
    }
    private string _CollectionDescription = string.Empty;
    public string CollectionDescription
    {
      get { return _CollectionDescription; }
      set
      {
        _CollectionDescription = value;
        OnPropertyChanged("CollectionDescription");
      }
    }

    public ScreenshotCollectionViewModel()
    {
      CollectionList = CollectionDataAccess.GetAllCollections();
    }

    private int _CollectionId;
    public int CollectionId
    {
      get { return _CollectionId; }
      set
      {
        _CollectionId = value;
        OnPropertyChanged("CollectionId");
      }
    }

    public void EditCollection()
    {
      CollectionName = SelectedCollection.CollectionName;
      CollectionPath = SelectedCollection.CollectionPath;
      CollectionDescription = SelectedCollection.CollectionDescription;
      CollectionId = SelectedCollection.Id;
    }

#pragma warning disable CA1822 // Mark members as static
    public void DeleteCollection()
#pragma warning restore CA1822 // Mark members as static
    {
      // TODO
    }

    public void SaveCollection()
    {
      if (CollectionId < 1)
      {
        var newCollection = new CollectionModel
        {
          CollectionName = CollectionName,
          CollectionPath = TextHelper.AddBackslash(CollectionPath),
          CollectionDescription = CollectionDescription
        };
        newCollection.Id = CollectionDataAccess.InsertCollection(newCollection);
        CollectionList.Add(newCollection);
        CollectionId = newCollection.Id;
        // https://briancaos.wordpress.com/2018/11/13/c-async-fire-and-forget/
        Task.Factory.StartNew(ImageManager.LoadNewImagesForAllCollectionsAsync);
        ClearCollection();
      }
      else
      {
        SelectedCollection.CollectionName = CollectionName;
        // Note: you cannot change the path, because it is totally unclear what that would mean
        SelectedCollection.CollectionPath = TextHelper.AddBackslash(SelectedCollection.CollectionPath); // TODO remove this later, not needed
        SelectedCollection.CollectionDescription = CollectionDescription;
        CollectionDataAccess.UpdateCollection(SelectedCollection);
      }
    }

    public void ClearCollection()
    {
      CollectionName = string.Empty;
      CollectionPath = string.Empty;
      CollectionDescription = string.Empty;
      CollectionId = 0;
      SelectedCollection = null;
    }
  }
}

