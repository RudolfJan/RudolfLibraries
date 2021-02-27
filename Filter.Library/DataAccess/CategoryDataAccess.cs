using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Library.Filters.Models;

namespace Filter.Library.Filters.DataAccess
  {
  public class CategoryDataAccess
    {
    public static List<CategoryModel> GetAllCategories()
      {
      var sql = "SELECT * FROM Categories ORDER BY CategoryName ASC";
      return DbAccess.LoadData<CategoryModel, dynamic>(sql, new { });
      }
    public static CategoryModel GetCategoryById(int categoryId)
      {
      var sql = "SELECT * FROM Categories WHERE Id= @categoryId";
      return DbAccess.LoadData<CategoryModel, dynamic>(sql, new { categoryId}).FirstOrDefault();
      }
    public static CategoryModel GetCategoryByName(string categoryName)
      {
      var sql = "SELECT * FROM Categories WHERE CategoryName= @categoryName";
      return DbAccess.LoadData<CategoryModel, dynamic>(sql, new { categoryName }).FirstOrDefault();
      }
    public static int InsertCategory(CategoryModel Category)
      {
      var sql = $"INSERT OR IGNORE INTO Categories (CategoryName, CategoryDescription) " +
                $"VALUES(@CategoryName, @CategoryDescription);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { Category.CategoryName, Category.CategoryDescription });
      }

    public static int UpdateCategory(CategoryModel Category)
      {
      var sql = "UPDATE OR IGNORE Categories SET CategoryName=@CategoryName, CategoryDescription=@CategoryDescription " +
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
