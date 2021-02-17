using Screenshots.Library.Models;
using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Screenshots.Library.DataAccess
  {
  public class CollectionDataAccess
    {
    public static List<CollectionModel> GetAllCollections()
      {
      var sql = "SELECT * FROM Collections";
      return DbAccess.LoadData<CollectionModel, dynamic>(sql, new { });
      }

    public static CollectionModel GetCollectionById(int collectionId)
      {
      var sql = "SELECT * FROM Collections WHERE Id=@collectionId";
      return DbAccess.LoadData<CollectionModel, dynamic>(sql, new {collectionId }).FirstOrDefault();
      }

    public static int InsertCollection(CollectionModel collection)
      {
      var sql = $"INSERT OR IGNORE INTO Collections (CollectionName, CollectionPath, CollectionDescription) " +
                $"VALUES(@CollectionName, @CollectionPath, @CollectionDescription);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { collection.CollectionName, collection.CollectionPath, collection.CollectionDescription });
      }

    public static int UpdateCollection(CollectionModel collection)
      {
      var sql = "UPDATE OR IGNORE Collections SET CollectionName=@CollectionName, CollectionPath=@CollectionPath, CollectionDescription=@CollectionDescription" +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { collection.CollectionName, collection.CollectionPath, collection.CollectionDescription, collection.Id });
      }

    public static void DeleteCollection(int id)
      {
      var sql = "DELETE FROM Collections WHERE Id=@id";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }
    }
  }
  
