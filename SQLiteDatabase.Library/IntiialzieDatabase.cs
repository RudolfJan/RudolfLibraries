using System.IO;

namespace SQLiteDatabase.Library
  {
  public static class DatabaseSetup
    {
    public static void InitDatabase(string Path, string DatabaseName = "Database.db", int DatabaseVersion = 1, string DatabaseVersionDescription = "Initial version")
      {
      var databasePath = $"{Path}\\{DatabaseName}";
      var connectionString = $"Data Source = {databasePath}; Version = 3;";
      IDatabaseFactory factory = new DatabaseFactory();
      DbManager.InitDatabase(connectionString, databasePath, factory);
      DbManager.UpdateDatabaseVersionNumber(DatabaseVersion, DatabaseVersionDescription);
      }
    }

  public class DatabaseFactory : IDatabaseFactory
    {
    public int CreateStructures()
      {
      foreach (var file in Directory.EnumerateFiles("SQL", "*.sql*"))
        {
        DbManager.CreateStructureElementFromFile(file);
        }
      return 0;
      }
    }
  }
