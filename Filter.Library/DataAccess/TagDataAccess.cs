using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Utilities.Library.Filters.Models;

namespace Utilities.Library.Filters.DataAccess
  {
  public class TagDataAccess
    {
    public static List<TagModel> GetAllTags()
      {
      var sql = "SELECT * FROM Tags";
      return DbAccess.LoadData<TagModel, dynamic>(sql, new { });
      }
 

    public static int InsertTag(TagModel tag)
      {
      var sql = $"INSERT OR IGNORE INTO Tags (TagName, TagDescription, CategoryId) " +
                $"VALUES(@TagName, @TagDescription, @CategoryId);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { tag.TagName, tag.TagDescription, tag.CategoryId});
      }

    public static int UpdateTag(TagModel tag)
      {
      var sql = "UPDATE OR IGNORE Tags SET TagName=@TagName, TagDescription=@TagDescription, CategoryId=@CategoryId " +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { tag.TagName, tag.TagDescription, tag.CategoryId, tag.Id });
      }

    public static void DeleteTag(int id)
      {
      var sql = "DELETE FROM Tags WHERE Id=@id";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }
    }
  }
