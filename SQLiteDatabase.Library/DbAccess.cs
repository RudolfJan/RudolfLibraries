using Dapper;
using Logging.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Library.Linq;

namespace SQLiteDatabase.Library
  {
  public class DbAccess
    {
    public const string LastRowInsertQuery= "SELECT last_insert_rowid()";
    public static void ClearTable(string tableName)
      {
      try
        {
        using IDbConnection DbConnection = new SQLiteConnection(DbManager.GetConnectionString());
        DbConnection.Execute($"DELETE FROM {tableName}");
        }
      catch (Exception ex)
        {
        Log.Trace($"Cannot clear table {tableName}", ex, LogEventType.Error);
        throw ex;
        }
      }

    public static List<T> LoadData<T, U>(string sqlStatement, U parameters)
      {
      try
        {
        using (IDbConnection connection = new SQLiteConnection(DbManager.GetConnectionString()))
          {
          List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
          return rows;
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Cannot execute query {sqlStatement}", ex, LogEventType.Error);
        throw ex;
        }
      }

    public static async Task<List<T>> LoadDataAsync<T, U>(string sqlStatement, U parameters)
      {
      try
        {
        using (IDbConnection connection = new SQLiteConnection(DbManager.GetConnectionString()))
          {
          var rows = await connection.QueryAsync<T>(sqlStatement, parameters);
          return await rows.ToListAsync();  // TODO find out why this is not working properly as extension method
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Cannot execute query {sqlStatement}", ex, LogEventType.Error);
        throw ex;
        }
      }
 
    public static int SaveData<T>(string sqlStatement, T parameters)
      {
      int output;
      try
        {
        using (IDbConnection connection = new SQLiteConnection(DbManager.GetConnectionString()))
          {
          output = connection.Query<int>(sqlStatement, parameters).FirstOrDefault();
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Cannot save data in database using {sqlStatement}", ex, LogEventType.Error);
        throw ex;
        }
      return output;
      }

    public static async Task<int> SaveDataAsync<T>(string sqlStatement, T parameters)
      {
      int output;
      try
        {
        using (IDbConnection connection = new SQLiteConnection(DbManager.GetConnectionString()))
          {
          output= await connection.ExecuteAsync(sqlStatement, parameters);
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Cannot save data in database using {sqlStatement}", ex, LogEventType.Error);
        throw ex;
        }
      return output;
      }
    }
  }
