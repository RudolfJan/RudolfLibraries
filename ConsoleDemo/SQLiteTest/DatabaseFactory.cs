using SQLiteDatabase.Library;

namespace ConsoleDemo.SQLiteTest
  {
  public class DatabaseFactory: IDatabaseFactory
    {
    public int CreateStructures()
      {
      DbManager.CreateStructureElementFromFile("SQLiteTest\\SQL\\PersonsTable.sql");
      return 0;
      }

    public static int CreateStructuresForVersion3()
      {
      DbManager.CreateStructureElementFromFile("SQLiteTest\\SQL\\ProfilesTable.sql");
      return 0;
      }

    public static int CreateStructuresForVersion4()
      {
      DbManager.CreateStructureElementFromFile("SQLiteTest\\SQL\\Version4Update.sql");
      // This assumes you update the PersonModel class and the data access functions as well
      // This is not Done for this demo, because the demo focues on preveting user impact, not correct working program logic.
      return 0;
      }
    }
  }
