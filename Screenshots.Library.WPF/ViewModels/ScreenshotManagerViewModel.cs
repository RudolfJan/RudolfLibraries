using Screenshots.Library.Models;
using Styles.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Screenshots.Library.DataAccess;
using Screenshots.Library.Logic;
using System.IO;
using System.Linq;
using Utilities.Library;
using Utilities.Library.Filters.Models;
using Filter.Library.Filters.DataAccess;

namespace Screenshots.Library.WPF.ViewModels
  {
  public class ScreenshotManagerViewModel : Notifier
    {
    public static int ScreenshotsPerPage { get; set; } = 16;
    public static int ScreenshotsPerRow { get; set; } = 4;
    public static int ScreenshotsPerColumn { get; set; } = 4;
    public static int NrOfTagsInFilter { get; set; } = 3;
    public static uint ThumbnailWidth { get; set; } = ThumbnailLogic.ThumbnailWidth;
    public static uint ThumbnailHeight { get; set; } = ThumbnailLogic.ThumbnailWidth*9/16;

    private List<ImageModel> _ScreenshotList = null;

    public List<ImageModel> ScreenshotList
      {
      get { return _ScreenshotList; }
      set
        {
        _ScreenshotList = value;
        OnPropertyChanged("ScreenshotList");
        }
      }

    private List<ImageModel> _FilteredScreenshotList;
    public List<ImageModel> FilteredScreenshotList
      {
      get { return _FilteredScreenshotList; }
      set
        {
        _FilteredScreenshotList = value;
        OnPropertyChanged("FilteredScreenshotList");
        }
      }

    private List<ImageModel> _DisplayScreenshotList = null;

    public List<ImageModel> DisplayScreenshotList
      {
      get { return _DisplayScreenshotList; }
      set
        {
        _DisplayScreenshotList = value;
        OnPropertyChanged("DisplayScreenshotList");
        }
      }

    private int _totalPageCount;

    public int TotalPageCount
      {
      get { return _totalPageCount; }
      set
        {
        _totalPageCount = value;
        OnPropertyChanged("TotalPageCount");
        }
      }

    private int _currentPage;
    public int CurrentPage
      {
      get { return _currentPage; }
      set
        {
        _currentPage = value;
        OnPropertyChanged("CurrentPage");
        }
      }

    private int _pageStartIndex = 0;
    public int PageStartIndex
      {
      get { return _pageStartIndex; }
      set
        {
        _pageStartIndex = value;
        OnPropertyChanged("PageStartIndex");
        }
      }
    private int _totalFilteredScreenshotCount;
    public int TotalFilteredScreenshotCount
      {
      get { return _totalFilteredScreenshotCount; }
      set
        {
        _totalFilteredScreenshotCount = value;
        TotalFilteredScreenshotCountDisplay = value - 1;
        OnPropertyChanged("TotalFilteredScreenshotCount");
        }
      }

    private int _totalFilteredScreenshotCountDisplay;
    public int TotalFilteredScreenshotCountDisplay
      {
      get { return _totalFilteredScreenshotCountDisplay; }
      set
        {
        _totalFilteredScreenshotCountDisplay = value;
        OnPropertyChanged("TotalFilteredScreenshotCountDisplay");
        }
      }

    private int _TotalScreenshotCount;
    public int TotalScreenshotCount
      {
      get { return _TotalScreenshotCount; }
      set
        {
        _TotalScreenshotCount = value;
        TotalScreenshotCountDisplay = value - 1;
        OnPropertyChanged("TotalScreenshotCount");
        }
      }

    private int _TotalScreenshotCountDisplay;
    public int TotalScreenshotCountDisplay
      {
      get { return _TotalScreenshotCountDisplay; }
      set
        {
        _TotalScreenshotCountDisplay = value;
        OnPropertyChanged("TotalScreenshotCountDisplay");
        }
      }
    private string _SelectedImagePath = string.Empty;

    public string SelectedImagePath
      {
      get { return _SelectedImagePath; }
      set
        {
        _SelectedImagePath = value;
        OnPropertyChanged("SelectedImagePath");
        }
      }

    public string FormattedPage
      {
      get { return $"{GetCurrentPageNumber()}/{GetTotalPageCount()}"; }
      }

    private bool _SelectUntaggedScreenshots;
    public bool SelectUntaggedScreenshots
      {
      get { return _SelectUntaggedScreenshots; }
      set
        {
        _SelectUntaggedScreenshots = value;
        OnPropertyChanged("SelectUntaggedScreenshots");
        }
      }


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

    private List<CategoryModel> _categoryList;
    public List<CategoryModel> CategoryList
      {
      get { return _categoryList; }
      set
        {
        _categoryList = value;
        OnPropertyChanged("CategoryList");
        }
      }

    private CategoryModel _SelectedCategory;
    public CategoryModel SelectedCategory
      {
      get { return _SelectedCategory; }
      set
        {
        _SelectedCategory = value;
        OnPropertyChanged("SelectedCategory");
        }
      }

    private List<TagCategoriesExtendedModel> _ExtendedTagList;
    public List<TagCategoriesExtendedModel> ExtendedTagList
      {
      get { return _ExtendedTagList; }
      set
        {
        _ExtendedTagList = value;
        OnPropertyChanged("ExtendedTagList");
        }
      }

    private List<TagCategoriesExtendedModel> _FilteredTagList;
    public List<TagCategoriesExtendedModel> FilteredTagList
      {
      get { return _FilteredTagList; }
      set
        {
        _FilteredTagList = value;
        OnPropertyChanged("FilteredTagList");
        }
      }

    private TagCategoriesExtendedModel _SelectedTag;
    public TagCategoriesExtendedModel SelectedTag
      {
      get { return _SelectedTag; }
      set
        {
        _SelectedTag = value;
        OnPropertyChanged("SelectedTag");
        }
      }

    private string _TagPattern=string.Empty;
    public string TagPattern
      {
      get { return _TagPattern; }
      set
        {
        _TagPattern = value;
        OnPropertyChanged("TagPattern");
        }
      }

    public ScreenshotManagerViewModel()
      {
      var newImages = ImageManager.LoadNewImagesForAllCollectionsAsync();
      ThumbnailWidth = ThumbnailLogic.ThumbnailWidth;
      CollectionList = CollectionDataAccess.GetAllCollections();
      CategoryList = CategoryDataAccess.GetAllCategories();
      ExtendedTagList = TagCategoriesExtendedDataAccess.GetAllTagsAndCategories();
      UpdateTagFilter();
      ScreenshotList = ImageDataAccess.GetAllImages();
      FilteredScreenshotList = GetFilteredScreenshotList();
      TotalScreenshotCount = ScreenshotList.Count+1;
      TotalPageCount = GetTotalPageCount();
      PageStartIndex = 0;
      CurrentPage = GetCurrentPageNumber();
      DisplayScreenshotList = new List<ImageModel>();
      GetDisplayRange();
      }

    public List<ImageModel> GetFilteredScreenshotList()
      { 
      if (SelectUntaggedScreenshots && SelectedCollection==null)
        {
        FilteredScreenshotList = ImageDataAccess.GetUntaggedImages();
        }
      else if (SelectedTag != null && SelectedCollection != null)
        {
        FilteredScreenshotList =
          ImageDataAccess.GetImagesByTagAndCollection(SelectedTag.TagId, SelectedCollection.Id);
        }
      else if (SelectedTag != null && SelectedCollection == null)
        {
        FilteredScreenshotList =
          ImageDataAccess.GetImagesByTag(SelectedTag.TagId);
        }
      else if (SelectedCategory != null && SelectedCollection != null)
        {
        FilteredScreenshotList =
          ImageDataAccess.GetImagesByCategoryAndCollection(SelectedCategory.Id,SelectedCollection.Id);
        }
      else if (SelectedCategory != null)
        {
        FilteredScreenshotList =
          ImageDataAccess.GetImagesByCategory( SelectedCategory.Id);
        }
      else if (SelectedCollection != null)
        {
        FilteredScreenshotList =
          ImageDataAccess.GetImagesByCollectionId(SelectedCollection.Id, SelectUntaggedScreenshots);
        }
      else
        {
        FilteredScreenshotList = ImageDataAccess.GetAllImages();
        }

      TotalFilteredScreenshotCount = FilteredScreenshotList.Count+1;
      return FilteredScreenshotList;
      }

    public void UpdateTagFilter()
      {
      if (SelectedCategory == null)
        {
        FilteredTagList = ExtendedTagList
          .Where(x =>
            x.TagAndCategory.Contains(TagPattern, StringComparison.InvariantCultureIgnoreCase))
          .OrderBy(x => x.TagAndCategory).Take(NrOfTagsInFilter).ToList();
        }
      else
        {
        FilteredTagList = ExtendedTagList.Where(x=>x.CategoryId==SelectedCategory.Id)
          .Where(x =>
            x.TagAndCategory.Contains(TagPattern, StringComparison.InvariantCultureIgnoreCase))
          .OrderBy(x => x.TagAndCategory).Take(NrOfTagsInFilter).ToList();
        }
      }
    private int GetTotalPageCount()
      {
      return (TotalFilteredScreenshotCount-1) / ScreenshotsPerPage + 1;
      }

    private int GetCurrentPageNumber()
      {
      if (PageStartIndex > TotalFilteredScreenshotCount-1 - ScreenshotsPerPage)
        {
        return GetTotalPageCount();
        }
      return PageStartIndex / ScreenshotsPerPage + 1;
      }

    public void IncreaseDisplayPage()
      {
      PageStartIndex= Math.Min(PageStartIndex + ScreenshotsPerPage,TotalFilteredScreenshotCount-ScreenshotsPerPage);
      GetDisplayRange();
      }

    public void DecreaseDisplayPage()
      {
      PageStartIndex = Math.Max(PageStartIndex - ScreenshotsPerPage, 0);
      GetDisplayRange();
      }

    public void GetFirstPage()
      {
      PageStartIndex = 0;
      GetDisplayRange();
      }

    public void GetLastPage()
      {
      PageStartIndex = Math.Max(TotalFilteredScreenshotCount - ScreenshotsPerPage,0);
      GetDisplayRange();
      }

    public void GetDisplayRange()
      {
      DisplayScreenshotList.Clear();
      int upperLimit = Math.Min(PageStartIndex + ScreenshotsPerPage, TotalFilteredScreenshotCount-1);
      CurrentPage = GetCurrentPageNumber();
      for (int idx = PageStartIndex;
          idx < upperLimit;
          idx++)
        {
        var S = FilteredScreenshotList[idx];
        DisplayScreenshotList.Add(S);
        }
      }


   public void DeleteScreenshot(ImageModel screenshot)
      {
      ImageManager.DeleteImage(screenshot);
      ScreenshotList.Remove(screenshot);
      DisplayScreenshotList.Remove(screenshot);
      }
    }
  }
