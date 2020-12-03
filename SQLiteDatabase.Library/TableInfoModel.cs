using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteDatabase.Library
  {
 public class TableInfoModel
    {
    public int Id { get; set; }
    public string ColumnName { get; set; }
    public string ColumnType { get; set; }
    public string DefaultValue { get; set; }
    public bool IsNotNull { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsHidden { get; set; }
    }
  }
