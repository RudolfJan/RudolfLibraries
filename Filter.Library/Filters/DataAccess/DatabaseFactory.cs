using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filter.Library.Filters.DataAccess
  {
  public class DatabaseFactory: IDatabaseFactory
    {
    public int CreateStructures()
      {
      DbManager.CreateStructureElementFromFile("Filters\\SQL\\Filters.sql");
      return 0;
      }
    }
  }
