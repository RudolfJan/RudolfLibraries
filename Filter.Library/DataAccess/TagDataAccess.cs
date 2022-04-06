using Filter.Library.Filters.DataAccess;
using Logging.Library;
using Microsoft.VisualBasic.FileIO;
using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities.Library.Filters.Models;

namespace Utilities.Library.Filters.DataAccess
  {
  public class TagDataAccess
    {
    public static List<TagModel> GetAllTags()
      {
      var sql = "SELECT * FROM Tags";
      return DbAccess.LoadData<TagModel, dynamic>(sql, new { });
      }

    public static int InsertTag(TagModel tag)
      {
      var sql = $"INSERT OR IGNORE INTO Tags (TagName, TagDescription, CategoryId) " +
                $"VALUES(@TagName, @TagDescription, @CategoryId);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { tag.TagName, tag.TagDescription, tag.CategoryId });
      }

    public static int UpdateTag(TagModel tag)
      {
      var sql = "UPDATE OR IGNORE Tags SET TagName=@TagName, TagDescription=@TagDescription, CategoryId=@CategoryId " +
                $"WHERE Id= @Id; {DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { tag.TagName, tag.TagDescription, tag.CategoryId, tag.Id });
      }

    public static void DeleteTag(int id)
      {
      var sql = "DELETE FROM Tags WHERE Id=@id";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }

    public static void ImportTagsFromCsv(string tagsCsvFilePath)
      {
      // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array

      string categoryName = "";
      int categoryId = 0;
      try
        {
        using (TextFieldParser csvParser = new TextFieldParser(tagsCsvFilePath))
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
            var tag = new TagModel();
            if (length > 0)
              {
              if (string.CompareOrdinal(categoryName, fields[0]) != 0)
                {
                categoryName = fields[0];
                var category = CategoryDataAccess.GetCategoryByName(categoryName);
                if (category == null)
                  {
                  var result = Log.Trace($"Category not defined in database {categoryName}", LogEventType.Error); // Data issue
                  throw new InvalidDataException(result);
                  }
                categoryId = category.Id;
                }
              }

            tag.CategoryId = categoryId;
            if (length > 1)
              {
              tag.TagName = fields[1];
              }

            if (length > 2)
              {
              tag.TagDescription = fields[2];
              }

            if (tag.TagName.Length > 0)
              {
              InsertTag(tag);
              }
            }
          }
        }
      catch (Exception ex)
        {
        Log.Trace($"Error reading tag import {ex.Message}");
        }
      }
    }
  }
