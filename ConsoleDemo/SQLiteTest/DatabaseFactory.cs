using System;
using System.Collections.Generic;
using System.Text;
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
    }
  }
