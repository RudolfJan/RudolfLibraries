using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Library.Filters.Models;

namespace Filter.Library.Filters.DataAccess
  {
  public class CategoryDataAccess
    {
    public static List<CategoryModel> GetAllCategories()
      {
      var sql = "SELECT * FROM Categories";
      return DbAccess.LoadData<CategoryModel, dynamic>(sql, new { });
      }


    public static int InsertCategory(CategoryModel Category)
      {
      var sql = $"INSERT OR IGNORE INTO Categories (CategoryName, CategoryDescription) " +
                $"VALUES(@CategoryName, @CategoryDescription);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { Category.CategoryName, Category.CategoryDescription });
      }

    public static int UpdateCategory(CategoryModel Category)
      {
      var sql = "UPDATE OR IGNORE Categories SET CategoryName=@CategoryName, CategoryDescription=@CategoryDescription" +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { Category.CategoryName, Category.CategoryDescription, Category.Id });
      }

    public static void DeleteCategory(int id)
      {
      var sql = "DELETE FROM Categories WHERE Id=@id";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }
    }
  }
