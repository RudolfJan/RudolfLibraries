using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Library.Filters.Models;
using Utilities.Library.TextHelpers;

namespace Filter.Library.Filters.DataAccess
  {
  public class TagCategoriesExtendedDataAccess
    {
    public static List<TagCategoriesExtendedModel> GetAllTagsAndCategories()
      {
      var sql = "SELECT * FROM TagsAndCategoriesView";
      return DbAccess.LoadData<TagCategoriesExtendedModel, dynamic>(sql, new { });
      }

    public static List<TagCategoriesExtendedModel> GetTagsForCategories(int tagId)
      {
      var sql = "SELECT * FROM TagsAndCategoriesView WHERE TagId=@tagId";
      return DbAccess.LoadData<TagCategoriesExtendedModel, dynamic>(sql, new {tagId});
      }

    public static string GetTagsForImage(int imageId)
      {
      var sql = "SELECT * FROM TagsAndCategoriesView, ImageTags WHERE TagsAndCategoriesView.TagId=ImageTags.TagId AND ImageTags.ImageId= @imageId";
      var tagList= DbAccess.LoadData<TagCategoriesExtendedModel, dynamic>(sql, new { imageId });
      var str = new StringBuilder();
      foreach (var tag in tagList)
        {
        str = str.Append(tag.CategoryName).Append("-").Append(tag.TagName).Append("; ");
        }
      return str.ToString();
      }

    public static List<TagCategoriesExtendedModel> GetFilteredTagsAndCategories(string categoryFilter,string tagFilter)
      {
      var catFilter = TextHelper.ToLikeWildCard(categoryFilter);
      var tFilter = TextHelper.ToLikeWildCard(tagFilter);
      var sql = $"SELECT * FROM TagsAndCategoriesView WHERE TagName LIKE \'{tFilter}\'  ESCAPE \'\\\' AND CategoryName LIKE \'{catFilter}\' ESCAPE \'\\\'";
      return DbAccess.LoadData<TagCategoriesExtendedModel, dynamic>(sql, new {tFilter, catFilter });
      }

    public static string GetSingleTag(int imageTagId)
      {
      var tagList = GetTagListForImage(imageTagId);
      var str = new StringBuilder();
      foreach (var tag in tagList)
        {
        str = str.Append(tag.CategoryName).Append("-").Append(tag.TagName).Append("; ");
        }
      return str.ToString();
      }

    public static List<TagCategoriesExtendedModel> GetTagListForImage(int imageId)
      {
      var sql = "SELECT * FROM TagsAndCategoriesView, ImageTags WHERE TagsAndCategoriesView.TagId=ImageTags.TagId AND ImageTags.ImageId= @imageId";
      return DbAccess.LoadData<TagCategoriesExtendedModel, dynamic>(sql, new { imageId });
      }
    }
  }
