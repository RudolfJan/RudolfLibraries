using Filter.Library.Filters.DataAccess;
using Logging.Library;
using Screenshots.Library.DataAccess;
using Screenshots.Library.Logic;
using Screenshots.Library.Models;
using Styles.Library.Helpers;
using Styles.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Utilities.Library.Filters.DataAccess;
using Utilities.Library.Filters.Models;

namespace Screenshots.Library.WPF.ViewModels
  {
  public class ScreenshotViewModel: Notifier
    {
    private ImageModel _Image;
    public ImageModel Image
      {
      get { return _Image; }
      set
        {
        _Image = value;
        OnPropertyChanged("Image");
        }
      }

    private CollectionModel _Collection;
    public CollectionModel Collection
      {
      get { return _Collection; }
      set
        {
        _Collection = value;
        OnPropertyChanged("Collection");
        }
      }

    private string _ImagePath;
    public string ImagePath
      {
      get { return _ImagePath; }
      set
        {
        _ImagePath = value;
        OnPropertyChanged("ImagePath");
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

    private List<TagCategoriesExtendedModel> _AvailableTagList;
    public List<TagCategoriesExtendedModel> AvailableTagList
      {
      get { return _AvailableTagList; }
      set
        {
        _AvailableTagList = value;
        OnPropertyChanged("AvailableTagList");
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

    private List<TagCategoriesExtendedModel> _ImageTagList;
    public List<TagCategoriesExtendedModel> ImageTagList
      {
      get { return _ImageTagList; }
      set
        {
        _ImageTagList = value;
        OnPropertyChanged("ImageTagList");
        }
      }

    private TagCategoriesExtendedModel _SelectedImageTag;
    public TagCategoriesExtendedModel SelectedImageTag
      {
      get { return _SelectedImageTag; }
      set
        {
        _SelectedImageTag = value;
        OnPropertyChanged("SelectedImageTag");
        }
      }

    // status field, needed to communicate between the screenshot collection
    // management and an individual screenshot, maybe a bit clumsy, but should work
    public bool IsDeleted { get; set; } = false;

    public ScreenshotViewModel(ImageModel image)
      {
      Image=image;
      Collection = CollectionDataAccess.GetCollectionById(Image.CollectionId);
      ImagePath = $"{Collection.CollectionPath}{Image.ImagePath}";
      AvailableTagList = TagCategoriesExtendedDataAccess.GetAllTagsAndCategories();
      ImageTagList= TagCategoriesExtendedDataAccess.GetTagListForImage(Image.Id);
      UpdateTagFilter();
      }

    public void UpdateTagFilter()
      {
      FilteredTagList = AvailableTagList.Where(x => x.TagAndCategory.Contains(TagPattern, StringComparison.InvariantCultureIgnoreCase)).OrderBy(x=>x.TagAndCategory).ToList();
      }

    public void AddTag()
      {
      var imageTagId=ImageManager.AddTag(Image.Id, SelectedTag.TagId);
      SelectedTag=null;
      TagPattern=string.Empty;
      ImageTagList = TagCategoriesExtendedDataAccess.GetTagListForImage(Image.Id);
      }

    public void RemoveTag()
      {
      ImageTagsDataAccess.DeleteImageTagForImage(Image.Id,SelectedImageTag.TagId);
      ImageTagList = TagCategoriesExtendedDataAccess.GetTagListForImage(Image.Id);
      }

    public void SaveScreenshotToDisk()
      {
      SaveFileModel fileDetails = new SaveFileModel
        {
        InitialDirectory =
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        OverWriteprompt = true,
        Title = "Save copy of screenshot here",
        Filter = "Jpeg files|*.jpg|Png files|*.png|All Files|*.*",
        DefaultExt = ""
        };
      var filePath = FileIOHelpers.GetSaveFileName(fileDetails);
      if (filePath.Length > 0)
        {
        File.Copy(ImagePath, filePath, true);
        Log.Trace($"Saved selected screenshot to {filePath}");
        }
      }

    public void DeleteScreenshot()
      {
      IsDeleted=true;
      ImageTagList.Clear();
      }
    }
  }
