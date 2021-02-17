using Screenshots.Library.Models;
using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Screenshots.Library.DataAccess
  {
  public class ImageDataAccess
    {
    public static List<ImageModel> GetAllImages()
      {
      var sql = "SELECT * FROM Images";
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { });
      }

    public static List<ImageModel> GetImagesByCollectionId(int collectionId)
      {
      var sql = "SELECT * FROM Images WHERE Images.CollectionId= @collectionId";
      return DbAccess.LoadData<ImageModel, dynamic>(sql, new { collectionId});
      }
    
    public static int InsertImage(ImageModel image)
      {
      var sql = $"INSERT OR IGNORE INTO Images (ImagePath, ImageDescription, ImageThumbnailPath, CollectionId) " +
                $"VALUES(@ImagePath, @ImageDescription, @ImageThumbnailPath, @CollectionId);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { image.ImagePath, image.ImageDescription, image.ImageThumbnailPath,  image.CollectionId});
      }

    public static int UpdateImage(ImageModel image)
      {
      var sql = "UPDATE OR IGNORE Images SET ImagePath=@ImagePath, ImageDescription=@ImageDescription, ImageThumbnailPath=@ImageThumbnailPath, CollectionId=@CollectionId" +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { image.ImagePath, image.ImageDescription, image.ImageThumbnailPath, image.CollectionId, image.Id });
      }

    public static void DeleteImage(int id)
      {
      var sql = "DELETE FROM Images WHERE Id=@id";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }

    public static void DeleteImagesInCollection(int collectionId)
      {
      var sql = "DELETE FROM Images WHERE ConnectionId=@collectionId";
      DbAccess.SaveData<dynamic>(sql, new { collectionId });
      }
    }
  }
