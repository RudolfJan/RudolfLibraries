using Screenshots.Library.DataAccess;
using Screenshots.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Styles.Library.Helpers;

namespace Screenshots.Library.Logic
  {
  public class CollectionManager : Notifier

    {
    private int collectionId;

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

    private string _CollectionDescription= string.Empty;
    public string CollectionDescription
      {
      get { return _CollectionDescription; }
      set
        {
        _CollectionDescription = value;
        OnPropertyChanged("CollectionDescription");
        }
      }

    public CollectionManager()
      {
      CollectionList = CollectionDataAccess.GetAllCollections();
      }

    public void SaveCollection()
      {

      if (collectionId == 0)
        {
        var newCollection = new CollectionModel
          {
          CollectionName = CollectionName,
          CollectionPath = CollectionPath,
          CollectionDescription = CollectionDescription
          };
        newCollection.Id=CollectionDataAccess.InsertCollection(newCollection);
        CollectionList.Add(newCollection);
        }
      else
        {
        SelectedCollection.CollectionName = CollectionName;
        SelectedCollection.CollectionPath = CollectionPath;
        SelectedCollection.CollectionDescription = CollectionDescription;
        CollectionDataAccess.UpdateCollection(SelectedCollection);
        }
      var newImages = ImageManager.LoadNewImagesForAllCollectionsAsync();
      }

    public void ClearCollection()
      {
      CollectionName= string.Empty;
      CollectionPath= string.Empty;
      CollectionDescription = string.Empty;
      collectionId = 0;
      }

    public void EditCollection()
      {
      CollectionName = SelectedCollection.CollectionName;
      CollectionPath = SelectedCollection.CollectionPath;
      CollectionDescription = SelectedCollection.CollectionDescription;
      collectionId= SelectedCollection.Id;
      }

    public void DeleteCollection()
      {
      CollectionDataAccess.DeleteCollection(SelectedCollection.Id);
      CollectionList.Remove(SelectedCollection);
      ClearCollection();
      SelectedCollection=null;
      }
		}
	}
