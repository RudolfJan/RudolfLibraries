using Filter.Library.Filters.DataAccess;
using Styles.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Utilities.Library.Filters.DataAccess;
using Utilities.Library.Filters.Models;

namespace Filter.Library.WPF.ViewModels
  {
  public class TagAndCategoryViewModel : Notifier
    {
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
        if (_SelectedCategory != null)
          {
          TagCategoryName = _SelectedCategory.CategoryName;
          }
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
  
    private List<TagCategoriesExtendedModel> _TagList;
    public List<TagCategoriesExtendedModel> TagList
      {
      get { return _TagList; }
      set
        {
        _TagList = value;
        OnPropertyChanged("TagList");
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

    private string _TagCategoryName=string.Empty;
    public string TagCategoryName
      {
      get { return _TagCategoryName; }
      set
        {
        _TagCategoryName = value;
        OnPropertyChanged("TagCategoryName");
        }
      }

    public TagAndCategoryViewModel()
      {
      var x = new CategoryModel
        {
        CategoryName = "Route",
        CategoryDescription = ""
        };
      var id=CategoryDataAccess.InsertCategory(x);
      CategoryList = CategoryDataAccess.GetAllCategories();
      var y = new TagModel
        {
        TagName = "RSN",
        TagDescription = "Ruhr-Sieg Nord"
        };
      var category = CategoryDataAccess.GetCategoryByName("Route");
      y.CategoryId= category.Id;
      var id2 = TagDataAccess.InsertTag(y);
      TagList = TagCategoriesExtendedDataAccess.GetAllTagsAndCategories();

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
      // TODO check that no Tags are still connected to the Category
      CategoryDataAccess.DeleteCategory(SelectedCategory.Id);
      CategoryList.Remove(SelectedCategory);
      ClearCategory();
      }

    public void EditTag()
      {
      TagName = SelectedTag.TagName;
      TagDescription = SelectedTag.TagDescription;
      var category = CategoryDataAccess.GetCategoryById(SelectedTag.CategoryId);
      TagCategoryName = category.CategoryName;
      _tagId=SelectedTag.TagId;
      }

    public void SaveTag()
      {
      if (_tagId < 1)
        {
        var newTag = new TagModel
          {
          TagName = TagName,
          TagDescription = TagDescription, 
          CategoryId= SelectedCategory.Id
          };
        newTag.Id = TagDataAccess.InsertTag(newTag);
        var tmp = TagCategoriesExtendedDataAccess.GetTagsForCategories(_tagId).FirstOrDefault();
        TagList.Add(tmp);
        }
      else
        {
        TagModel updatedTag = new TagModel
          {
          TagName = TagName,
          TagDescription = TagDescription
          };
        if (SelectedCategory != null)
          {
          updatedTag.CategoryId = SelectedCategory.Id;
          }
        TagDataAccess.UpdateTag(updatedTag);
        }
      }

    public void ClearTag()
      {
      TagName=string.Empty;
      TagDescription=string.Empty;
      TagCategoryName=string.Empty;
      ClearCategory();
      _tagId = 0;
      SelectedTag=null;
      }

    public void DeleteTag()
      {
      TagDataAccess.DeleteTag(SelectedTag.TagId);
      TagList.Remove(SelectedTag);
      ClearTag();
      }
		}
	}
