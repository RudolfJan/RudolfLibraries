using Screenshots.Library.Models;
using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Screenshots.Library.DataAccess
  {
  public class ImageTagsDataAccess
    {
    public static List<ImageTagsModel> GetAllImageTags()
      {
      var sql = "SELECT * FROM ImageTags";
      return DbAccess.LoadData<ImageTagsModel, dynamic>(sql, new { });
      }

    public static string GetTagsForImage(int imageId)
      {
      var sql = "SELECT TagName FROM ImageTags, Tags WHERE ImageId=@imageId";
      var tagList= DbAccess.LoadData<List<string>, dynamic>(sql, new { imageId});
      StringBuilder output = new StringBuilder();
      foreach (var tag in tagList)
        {
        if (output.Length > 0)
          {
          output.Append(", ");
          }
        output.Append(tag);
        }
      return output.ToString();
      }

    public static int InsertImageTag(ImageTagsModel imageTag)
      {
      var sql = $"INSERT OR IGNORE INTO ImageTags (ImageId, TagId) " +
                $"VALUES(@ImageId, @TagId);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { imageTag.ImageId, imageTag.TagId });
      }

    public static int UpdateImageTag(ImageTagsModel imageTag)
      {
      var sql = "UPDATE OR IGNORE ImageTags SET ImageId=@ImageId, TagId=@TagId" +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { imageTag.ImageId, imageTag.TagId, imageTag.Id });
      }

    public static void DeleteImageTag(int id)
      {
      var sql = "DELETE FROM ImageTags WHERE Id=@id";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }

    public static void DeleteImageTagForImage(int imageId, int tagId)
      {
      var sql = "DELETE FROM ImageTags WHERE ImageId=@imageId AND Tagid=@tagId";
      DbAccess.SaveData<dynamic>(sql, new { imageId, tagId });
      }
    }
  }
