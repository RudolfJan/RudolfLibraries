using Filter.Library.Filters.DataAccess;
using Styles.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Utilities.Library.Filters.DataAccess;
using Utilities.Library.Filters.Models;

namespace Filter.Library.WPF.ViewModels
  {
  public class TagAndCategoryViewModel : Notifier
    {
    private const string PropertyName = "TagCategoriesList";
    private int _categoryId;
    private int _tagId;

    private List<CategoryModel> _CategoryList;
    public List<CategoryModel> CategoryList
      {
      get { return _CategoryList; }
      set
        {
        _CategoryList = value;
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


    private string _CategoryName=string.Empty;
    public string CategoryName
      {
      get { return _CategoryName; }
      set
        {
        _CategoryName = value;
        OnPropertyChanged("CategoryName");
        }
      }

    private string _CategoryDescription=string.Empty;
    public string CategoryDescription
      {
      get { return _CategoryDescription; }
      set
        {
        _CategoryDescription = value;
        OnPropertyChanged("CategoryDescription");
        }
      }

  
    private List<TagModel> _TagList;
    public List<TagModel> TagList
      {
      get { return _TagList; }
      set
        {
        _TagList = value;
        OnPropertyChanged("TagList");
        }
      }

    private TagModel _SelectedTag;
    public TagModel SelectedTag
      {
      get { return _SelectedTag; }
      set
        {
        _SelectedTag = value;
        OnPropertyChanged("SelectedTag");
        }
      }

    private string _TagName=string.Empty;
    public string TagName
      {
      get { return _TagName; }
      set
        {
        _TagName = value;
        OnPropertyChanged("TagName");
        }
      }

    private string _TagDescription=string.Empty;
    public string TagDescription
      {
      get { return _TagDescription; }
      set
        {
        _TagDescription = value;
        OnPropertyChanged("TagDescription");
        }
      }

    private List<TagCategoriesExtendedModel> _TagCategoriesList;
    public List<TagCategoriesExtendedModel> TagCategoriesList
      {
      get { return _TagCategoriesList; }
      set
        {
        _TagCategoriesList = value;
        OnPropertyChanged("TagCategoriesList");
        }
      }
    private TagCategoriesExtendedModel _SelectedTagCategory;
    public TagCategoriesExtendedModel SelectedTagCategory
      {
      get { return _SelectedTagCategory; }
      set
        {
        _SelectedTagCategory = value;
        OnPropertyChanged("SelectedTagCategory");
        }
      }


    public TagAndCategoryViewModel()
      {
      var x = new CategoryModel
        {
        CategoryName = "Routes",
        CategoryDescription = "a description"
        };
      var id=CategoryDataAccess.InsertCategory(x);
      CategoryList = CategoryDataAccess.GetAllCategories();
      var y = new TagModel
        {
        TagName = "RSN",
        TagDescription = "Ruhr-Sieg Nord"
        };
      var id2 = TagDataAccess.InsertTag(y);
      TagList = TagDataAccess.GetAllTags();
      
      }

    public void EditCategory()
      {
      CategoryName = SelectedCategory.CategoryName;
      CategoryDescription = SelectedCategory.CategoryDescription;
      _categoryId=SelectedCategory.Id;
      }

    public void SaveCategory()
      {
      if (_categoryId < 1)
        {
        var newCategory = new CategoryModel
          {
          CategoryName = CategoryName,
          CategoryDescription = CategoryDescription
          };
        newCategory.Id=CategoryDataAccess.InsertCategory(newCategory);
        CategoryList.Add(newCategory);
        }
      else
        {
        SelectedCategory.CategoryName = CategoryName;
        SelectedCategory.CategoryDescription = CategoryDescription;
        CategoryDataAccess.UpdateCategory(SelectedCategory);
        }
      }

    public void ClearCategory()
      {
      CategoryName=string.Empty;
      CategoryDescription = string.Empty;
      _categoryId = 0;
      SelectedCategory=null;
      }

    public void DeleteCategory()
      {
      CategoryDataAccess.DeleteCategory(SelectedCategory.Id);
      CategoryList.Remove(SelectedCategory);
      ClearCategory();
      }

    public void EditTag()
      {
      TagName = SelectedTag.TagName;
      TagDescription = SelectedTag.TagDescription;
      _tagId=SelectedTag.Id;
      }

    public void SaveTag()
      {
      if (_tagId < 1)
        {
        var newTag = new TagModel
          {
          TagName = TagName,
          TagDescription = TagDescription
          };
        newTag.Id = TagDataAccess.InsertTag(newTag);
        TagList.Add(newTag);
        }
      else
        {
        SelectedTag.TagName=TagName;
        SelectedTag.TagDescription = TagDescription;
        TagDataAccess.UpdateTag(SelectedTag);
        }
      }

    public void ClearTag()
      {
      TagName=string.Empty;
      TagDescription=string.Empty;
      _tagId = 0;
      SelectedTag=null;
      }

    public void DeleteTag()
      {
      TagDataAccess.DeleteTag(SelectedTag.Id);
      TagList.Remove(SelectedTag);
      ClearTag();
      }

    public void SetCategoriesPerTagList()
      {
      TagCategoriesList = TagCategoriesExtendedDataAccess.GetTagsForCategories(SelectedTag.Id);
      }

    internal void UnSetCategoriesPerTagList()
      {
      TagCategoriesList=null;
      SelectedTagCategory=null;
      }

    public void AddCategoryToTag()
      {
      var t = new TagCategoriesModel
        {
        TagId = SelectedTag.Id,
        CategoryId = SelectedCategory.Id
        };
      TagCategoriesDataAccess.InsertTagCategory(t);
      SetCategoriesPerTagList();
      }

    public void RemoveCategoryFromTag()
      {
      TagCategoriesDataAccess.DeleteTagCategory(SelectedTagCategory.TagId,SelectedTagCategory.CategoryId);
      SetCategoriesPerTagList();
      }
		}
	}
