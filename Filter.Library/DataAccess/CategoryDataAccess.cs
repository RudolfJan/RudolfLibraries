using Logging.Library;
using Microsoft.VisualBasic.FileIO;
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

    public static void ImportCategoriesFromCsv(string categoriesCsvFilePath)
      {
      // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array

      try
        {
        using (TextFieldParser csvParser = new TextFieldParser(categoriesCsvFilePath))
          {
          csvParser.CommentTokens = new string[] { "#" };
          csvParser.SetDelimiters(new string[] { "," });
          csvParser.HasFieldsEnclosedInQuotes = true;

          // Skip the row with the column names
          csvParser.ReadLine();

          while (!csvParser.EndOfData)
            {
            // Read current line fields, pointer moves to the next line.
            string[] fields = csvParser.ReadFields();
            int length = fields.GetLength(0);
            CategoryModel category = new CategoryModel();
            if (length > 0)
              {
              category.CategoryName = fields[0];
              }

            if (length > 1)
              {
              category.CategoryDescription = fields[1];
              }
            InsertCategory(category);
            }
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Error reading category import {ex.Message}");
        }
      }
    }
  }
