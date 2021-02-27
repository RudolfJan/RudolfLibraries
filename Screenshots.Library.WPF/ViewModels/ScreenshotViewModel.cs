using Filter.Library.Filters.DataAccess;
using Screenshots.Library.DataAccess;
using Screenshots.Library.Models;
using Styles.Library.Helpers;
using System;
using System.Collections.Generic;
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

    private string _Tags;
    public string Tags
      {
      get { return _Tags; }
      set
        {
        _Tags = value;
        OnPropertyChanged("Tags");
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


    public ScreenshotViewModel(ImageModel image)
      {
      Image=image;
      Collection = CollectionDataAccess.GetCollectionById(Image.CollectionId);
      ImagePath = $"{Collection.CollectionPath}{Image.ImagePath}";
      Tags = TagCategoriesExtendedDataAccess.GetTagsForImage(Image.Id);
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
      var imageTag = new ImageTagsModel
        {
        ImageId = Image.Id,
        TagId = SelectedTag.TagId
        };
      var imageTagId= ImageTagsDataAccess.InsertImageTag(imageTag);
      SelectedTag=null;
      TagPattern=string.Empty;
      ImageTagList = TagCategoriesExtendedDataAccess.GetTagListForImage(Image.Id);
      Tags += TagCategoriesExtendedDataAccess.GetSingleTag(imageTagId);
      }

    public void RemoveTag()
      {
      ImageTagsDataAccess.DeleteImageTagForImage(Image.Id,SelectedImageTag.TagId);
      ImageTagList = TagCategoriesExtendedDataAccess.GetTagListForImage(Image.Id);
      }
    }
  }
