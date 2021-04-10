using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Screenshots.Library.DataAccess
  {
  public class DatabaseFactory: IDatabaseFactory
    {
    public int CreateStructures()
      {
      DbManager.CreateStructureElementFromFile("SQL\\Filters.sql");
      DbManager.CreateStructureElementFromFile("SQL\\Screenshots.sql");
      return 0;
      }
    }
  }
