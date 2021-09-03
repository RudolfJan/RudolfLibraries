using Screenshots.Library.Models;
using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Screenshots.Library.DataAccess
  {
  public class ImageDataAccess
    {
    public static List<ImageModel> GetAllImages()
      {
      var sql = "SELECT * FROM Images";
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { });
      }

    // https://stackoverflow.com/questions/4076098/how-to-select-rows-with-no-matching-entry-in-another-table
    public static List<ImageModel> GetUntaggedImages()
      {
      var sql = @"SELECT DISTINCT Images.Id, Images.ImagePath, Images.ImageDescription, Images.ImageThumbnailPath, Images.CollectionId FROM Images 
                      LEFT JOIN ImageTags ON Images.Id = ImageTags.ImageId
                      WHERE ImageTags.ImageId IS NULL";
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { });
      }

    public static List<ImageModel> GetImagesByCollectionId(int collectionId, bool SelectUntagged=false)
      {
      string sql;
      if (SelectUntagged)
        {
        sql = @"SELECT DISTINCT Images.Id, Images.ImagePath, Images.ImageDescription, Images.ImageThumbnailPath, Images.CollectionId FROM Images
                      LEFT JOIN ImageTags ON Images.Id = ImageTags.ImageId
                      WHERE ImageTags.ImageId IS NULL AND Images.CollectionId= @collectionId";
        }
      else
        {
        sql = "SELECT * FROM Images WHERE Images.CollectionId= @collectionId";
        }

      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { collectionId});
      }

    public static List<ImageModel> GetImagesByCategory(int categoryId)
      {
      var sql = @"SELECT DISTINCT Images.Id, Images.ImagePath, Images.ImageDescription, Images.ImageThumbnailPath, Images.CollectionId 
                        FROM Images 
                        LEFT JOIN ImageTags, Tags 
                            ON Images.Id = ImageTags.ImageId 
                            AND ImageTags.TagId = Tags.Id 
                            AND Tags.CategoryId = @categoryId;";
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { categoryId});
      }
    public static List<ImageModel> GetImagesByCategoryAndCollection(int categoryId, int collectionId)
      {
      var sql = @"SELECT DISTINCT Images.Id, Images.ImagePath, Images.ImageDescription, Images.ImageThumbnailPath, Images.CollectionId 
                        FROM Images 
                        LEFT JOIN ImageTags, Tags 
                            ON Images.Id = ImageTags.ImageId 
                            AND ImageTags.TagId = Tags.Id 
                            AND Tags.CategoryId = @categoryId
                            AND Images.CollectionId= @collectionId;"; 
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new {categoryId,  collectionId });
      }

    public static List<ImageModel> GetImagesByTagAndCollection(int tagId, int collectionId)
      {
      var sql = @"SELECT DISTINCT Images.Id, Images.ImagePath, Images.ImageDescription, Images.ImageThumbnailPath, Images.CollectionId 
                        FROM Images 
                        LEFT JOIN ImageTags, Tags 
                            ON Images.Id = ImageTags.ImageId 
                            AND ImageTags.TagId = Tags.Id 
                            AND Images.CollectionId= @collectionId
                            AND ImageTags.TagId=@tagId;";
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { tagId, collectionId });
      }

    public static List<ImageModel> GetImagesByTag(int tagId)
      {
      var sql = @"SELECT DISTINCT Images.Id, Images.ImagePath, Images.ImageDescription, Images.ImageThumbnailPath, Images.CollectionId 
                        FROM Images 
                        LEFT JOIN ImageTags, Tags 
                            ON Images.Id = ImageTags.ImageId 
                            AND ImageTags.Tagid=@tagId;";
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { tagId});
      }

    public static int InsertImage(ImageModel image)
      {
      var sql = $"INSERT OR IGNORE INTO Images (ImagePath, ImageDescription, ImageThumbnailPath, CollectionId) " +
                $"VALUES(@ImagePath, @ImageDescription, @ImageThumbnailPath, @CollectionId);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { image.ImagePath, image.ImageDescription, image.ImageThumbnailPath,  image.CollectionId});
      }

    public static Task<int> InsertImageAsync(ImageModel image)
      {
      var sql = $"INSERT OR IGNORE INTO Images (ImagePath, ImageDescription, ImageThumbnailPath, CollectionId) " +
                $"VALUES(@ImagePath, @ImageDescription, @ImageThumbnailPath, @CollectionId);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveDataAsync<dynamic>(sql, new { image.ImagePath, image.ImageDescription, image.ImageThumbnailPath, image.CollectionId });
      }

    public static int UpdateImage(ImageModel image)
      {
      var sql = "UPDATE OR IGNORE Images SET ImagePath=@ImagePath, ImageDescription=@ImageDescription, ImageThumbnailPath=@ImageThumbnailPath, CollectionId=@CollectionId" +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { image.ImagePath, image.ImageDescription, image.ImageThumbnailPath, image.CollectionId, image.Id });
      }

    public static void DeleteImage(int id)
      {
      var sql = "DELETE FROM Images WHERE Id=@id;";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }

    public static void DeleteImagesInCollection(int collectionId)
      {
      var sql = "DELETE FROM Images WHERE ConnectionId=@collectionId";
      DbAccess.SaveData<dynamic>(sql, new { collectionId });
      }
    }
  }
