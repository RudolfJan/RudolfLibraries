// Ignore Spelling: sql

using Dapper;
using Logging.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace SQLiteDatabase.Library
  {
  public class DbManager
    {
    #region Properties

    public static int CurrentDatabaseVersion { get; set; } = 1;
    public static string DatabaseVersionDescription { get; set; } = "Initial database version";

    private static string _databasePath;
    private static string _connectionString;


    #endregion

    #region Constructors

    protected static void CreateDatabase()
      {
      try
        {
        if (!File.Exists(_databasePath))
          {
          var dir = Path.GetDirectoryName(_databasePath);
          if (!string.IsNullOrWhiteSpace(dir))
            {
            Directory.CreateDirectory(dir);
            }
          SQLiteConnection.CreateFile(_databasePath);
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Exception during creating Database{_databasePath}", ex, LogEventType.Error);
        throw;
        }
      }

    public static void DeleteDatabase(string databasePath = null)
      {
      if (!string.IsNullOrEmpty(databasePath))
        {
        _databasePath = databasePath;
        }
      if (string.IsNullOrWhiteSpace(_databasePath))
        {
        throw new ArgumentException("Database path not set");
        }

      if (File.Exists(_databasePath))
        {
        File.Delete(_databasePath);
        }
      }

    public static int InitDatabase(string connectionString, string databasePath, IDatabaseFactory factory)
      {
      _connectionString = connectionString;
      _databasePath = databasePath;
      CreateDatabase();
      CreateVersionTable();
      return factory.CreateStructures(); // this method is your interface to create tables and initial data.
      }

    public static void CreateStructureElementFromFile(string filePathSQL)
      {
      try
        {
        string sqlStatement = File.ReadAllText(filePathSQL);
        CreateStructureElement(sqlStatement);
        }
      catch (Exception ex)
        {
        Log.Trace($"Exception during create database table command {filePathSQL}", ex, LogEventType.Error);
        throw;
        }
      }

    private static int CreateStructureElement(string sqlStatement)
      {
      try
        {
        using IDbConnection DbConnection = new SQLiteConnection(GetConnectionString());
          {
          return DbConnection.Execute(sqlStatement);
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Exception during create database table command {sqlStatement}", ex, LogEventType.Error);
        throw;
        }
      }
    #endregion

    #region versionmanager

    public static void CreateVersionTable()
      {
      string sql = @"CREATE TABLE IF NOT EXISTS Version (
                  Id INTEGER NOT NULL PRIMARY KEY,
                  VersionNr INTEGER NOT NULL UNIQUE ON CONFLICT IGNORE,
                  Description TEXT NOT NULL
        );";
      int result = CreateStructureElement(sql);
      if (result >= 0)
        {
        UpdateDatabaseVersionNumber(CurrentDatabaseVersion, DatabaseVersionDescription);
        }
      }

    public static VersionModel GetCurrentVersion()
      {
      var version = new VersionModel();
      var sql = @"SELECT Id, VersionNr, Description FROM Version WHERE VersionNr = (SELECT MAX(VersionNr) FROM Version)";
      return DbAccess.LoadData<VersionModel, dynamic>(sql, new { }).FirstOrDefault();
      }

    public static List<VersionModel> GetVersionHistory()
      {
      var sql = "SELECT * FROM Version";
      return DbAccess.LoadData<VersionModel, dynamic>(sql, new { });
      }

    public static int UpdateDatabaseVersionNumber(int newVersion, string description)
      {
      var version = new VersionModel
        {
        VersionNr = newVersion,
        Description = description
        };
      var sqlStatement = "INSERT OR IGNORE INTO Version (VersionNr, Description) VALUES (@VersionNr, @Description); " +
                         DbAccess.LastRowInsertQuery;
      return DbAccess.SaveData<dynamic>(sqlStatement, new { version.VersionNr, version.Description });
      }

    #endregion

    public static List<TableInfoModel> GetTableInfo(string tableName)
      {
      var sql = $"SELECT cid AS Id, name as ColumnName, [type] AS ColumnType, [notnull] AS IsNotNull, dflt_value AS DefaultValue, pk AS IsPrimaryKey, hidden AS IsHidden FROM pragma_table_xinfo('{tableName}');";
      return DbAccess.LoadData<TableInfoModel, dynamic>(sql, new { });
      }

    #region info

    // https://stackoverflow.com/questions/1601151/how-do-i-check-in-sqlite-whether-a-table-exists
    public static bool TableExists(String TableName)
      {
      try
        {
        using (var DbConnection = new SQLiteConnection(_connectionString))
          {
          DbConnection.Open();
          // Create tables
          using (var Command = new SQLiteCommand("SELECT 1 FROM " + TableName + ";", DbConnection))
            {
            Command.ExecuteNonQuery();
            }
          return true;
          }
        }
      catch (SQLiteException)
        {
        return false;
        }
      }

    public static bool ColumnExists(string tableName, string columnName)
      {
      var sql = $"SELECT COUNT(*) AS RecordCount FROM pragma_table_info('{tableName}') WHERE name='{columnName}';";
      var result = DbAccess.LoadData<int, dynamic>(sql, new { }).FirstOrDefault();
      return result == 1;
      }

    #endregion

    public static string GetConnectionString()
      {
      if (string.IsNullOrEmpty(_connectionString))
        {
        throw new ArgumentException("Connection string not defined");
        }
      return _connectionString;
      }
    }
  }
