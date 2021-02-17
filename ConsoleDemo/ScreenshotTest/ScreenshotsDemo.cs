using Filter.Library.Filters.DataAccess;
using Logging.Library;
using Screenshots.Library.Logic;
using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Library.Filters.DataAccess;
using Utilities.Library.Filters.Models;

namespace ConsoleDemo
  {
  public class ScreenshotsDemo
    {
    public static void RunScreenshotsDemo()
      {
      string databasePath = "ScreenshotsDemo.db"; // will run in application folder
      string connectionString = $"Data Source = {databasePath}; Version = 3;";
      DatabaseSetupDemo(databasePath, connectionString);
      // This method will create a database if it does not exist and then initialize the database with tables and startup data.
      // For the latter you must implement the IDatabaseFactory interface
      TestCollectionSetup();
      TestImagesSetup();
      AddCategories();
      AddTags();
      LinkTagsAndCategories();
      }

    private static void DatabaseSetupDemo(string databasePath, string connectionString)
      {
      DbManager.DeleteDatabase(databasePath); // Make sure to set initial condition
      var factory = new Screenshots.Library.DataAccess.DatabaseFactory();
      DbManager.InitDatabase(connectionString, databasePath, factory);
      DbManager.InitDatabase(connectionString, databasePath, factory); // Test if code is re-entrant
      var version = DbManager.GetCurrentVersion();
      Console.WriteLine(
        $"Created database {databasePath} Version={version.VersionNr} {version.Description}\r\n");
      }

    private static void TestCollectionSetup()
      {
      var collectionManager = new CollectionManager
        {
        CollectionName = "TestCollection1",
        CollectionPath = "ScreenshotTest\\TestCollection1\\"
        };
      collectionManager.SaveCollection();
      collectionManager.CollectionName = "TestCollection2";
      collectionManager.CollectionPath = "ScreenshotTest\\TestCollection2\\";
      collectionManager.SaveCollection();
      foreach (var collection in collectionManager.CollectionList)
        {
        Console.WriteLine($"{collection.Id}: {collection.CollectionName} - {collection.CollectionPath}");
        }
      }

    private static void TestImagesSetup()
      {
      var imageManager = new ImageManager();
      ImageManager.ThumbnailBasePath = "C:\\Temp\\Thumbnails\\";
      ImageManager.LoadNewImagesForAllCollections();
      }

    private static void AddCategories()
      {
      var cat1 = new CategoryModel()
        {
        CategoryName = "Route",
        CategoryDescription = "This is a route"
        };
      var cat2 = new CategoryModel()
        {
        CategoryName = "Loco",
        CategoryDescription = "This is a loco"
        };
      var cat3 = new CategoryModel()
        {
        CategoryName = "Country",
        CategoryDescription = ""
        };
      CategoryDataAccess.InsertCategory(cat1);
      CategoryDataAccess.InsertCategory(cat1); //check if it is re-entrant
      CategoryDataAccess.InsertCategory(cat2);
      CategoryDataAccess.InsertCategory(cat3);
      var result = CategoryDataAccess.GetAllCategories();

      foreach (var cat in result)
        {
        Console.WriteLine($"{cat.Id} {cat.CategoryName} {cat.CategoryDescription}");
        Console.WriteLine();
        }
      }
    private static void AddTags()
      {
      var tag1 = new TagModel()
        {
        TagName = "SouthEastern Highspeed",
        TagDescription = "tag1 description"
        };
      var tag2 = new TagModel()
        {
        TagName = "Class 395",
        TagDescription = "tag2 description"
        };
      var tag3 = new TagModel()
        {
        TagName = "Class 375",
        TagDescription = "tag3 description"
        };
      var tag4 = new TagModel()
        {
        TagName = "UK",
        TagDescription = "tag4 description"
        };
      var tag5 = new TagModel()
        {
        TagName = "DE",
        TagDescription = ""
        };
      var tag6 = new TagModel()
        {
        TagName = "Main Spessart Bahn",
        TagDescription = ""
        };
      TagDataAccess.InsertTag(tag1);
      TagDataAccess.InsertTag(tag1);
      TagDataAccess.InsertTag(tag2);
      TagDataAccess.InsertTag(tag3);
      TagDataAccess.InsertTag(tag4);
      TagDataAccess.InsertTag(tag5);
      TagDataAccess.InsertTag(tag6);
      var result = TagDataAccess.GetAllTags();

      foreach (var tag in result)
        {
        Console.WriteLine($"{tag.Id} {tag.TagName} {tag.TagDescription}");
        Console.WriteLine();
        }
      }

    private static void LinkTagsAndCategories()
      {
      AddTagLink(1, 1);
      AddTagLink(2, 2);
      AddTagLink(3, 2);
      AddTagLink(4, 3);
      AddTagLink(5, 3);
      AddTagLink(6, 1);
      var result = TagCategoriesExtendedDataAccess.GetAllTagsAndCategories();
      foreach (var x in result)
        {
        Console.WriteLine(x.TagAndCategory);
        }


      result = TagCategoriesExtendedDataAccess.GetFilteredTagsAndCategories("*", "C*");
      foreach (var x in result)
        {
        Console.WriteLine(x.TagAndCategory);
        }
      }

    private static void AddTagLink(int tagId, int categoryId)
      {
      var t = new TagCategoriesModel
        {
        TagId = tagId,
        CategoryId = categoryId
        };

      TagCategoriesDataAccess.InsertTagCategory(t);
      }

    }
  }
