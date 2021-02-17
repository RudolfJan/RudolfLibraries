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

    public static List<TagCategoriesExtendedModel> GetFilteredTagsAndCategories(string categoryFilter,string tagFilter)
      {
      var catFilter = TextHelper.ToLikeWildCard(categoryFilter);
      var tFilter = TextHelper.ToLikeWildCard(tagFilter);
      var sql = $"SELECT * FROM TagsAndCategoriesView WHERE TagName LIKE \'{tFilter}\'  ESCAPE \'\\\' AND CategoryName LIKE \'{catFilter}\' ESCAPE \'\\\'";
      return DbAccess.LoadData<TagCategoriesExtendedModel, dynamic>(sql, new {tFilter, catFilter });
      }
    }
  }
