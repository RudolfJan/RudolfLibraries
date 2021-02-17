using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Library.Filters.Models;

namespace Filter.Library.Filters.DataAccess
  {
  public class TagCategoriesDataAccess
    {
    public static List<TagCategoriesModel> GetAllTagCategories()
      {
      var sql = "SELECT * FROM TagCategories";
      return DbAccess.LoadData<TagCategoriesModel, dynamic>(sql, new { });
      }


    public static int InsertTagCategory(TagCategoriesModel tagCategory)
      {
      var sql = $"INSERT OR IGNORE INTO TagCategories (TagId, CategoryId) " +
                $"VALUES(@TagId, @CategoryId);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { tagCategory.TagId, tagCategory.CategoryId });
      }

    public static int UpdateTagCategory(TagCategoriesModel tagCategory)
      {
      var sql = "UPDATE OR IGNORE TagCategories SET TagId=@TagId, CategoryId=@TagCategoryId" +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { tagCategory.TagId, tagCategory.CategoryId, tagCategory.Id });
      }

    public static void DeleteTagCategory(int tagId, int categoryId)
      {
      var sql = "DELETE FROM TagCategories WHERE TagId=@tagId AND CategoryId=@categoryId";
      DbAccess.SaveData<dynamic>(sql, new { tagId, categoryId });
      }
    }
  }
