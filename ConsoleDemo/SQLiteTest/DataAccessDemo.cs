using ConsoleDemo.SQLiteTest;
using ConsoleDemo.SQLiteTest.Models;
using SQLiteDatabase.Library;
using System;

namespace ConsoleDemo
  {
  public class DataAccessDemo
    {
    public static void SQLiteDatabaseDemo()
      {
      string databasePath = "Demo.db"; // will run in application folder
      string connectionString = $"Data Source = {databasePath}; Version = 3;";
      // This method will create a database if it does not exist and then initialize the database with tables and startup data.
      // For the latter you must implement the IDatabaseFactory interface

      DbManager.DeleteDatabase(databasePath); // Make sure to set initial condition
      var factory = new DatabaseFactory();
      DbManager.InitDatabase(connectionString, databasePath, factory);

      DbManager.InitDatabase(connectionString, databasePath, factory); // Test if code is re-entrant
      DbManager.UpdateDatabaseVersionNumber(2, "Updated version"); // test if we can update version
      var version = DbManager.GetCurrentVersion();
      Console.WriteLine(
        $"Created database {databasePath} Version={version.VersionNr} {version.Description}\r\n");

      var versionHistory = DbManager.GetVersionHistory();
      foreach (var item in versionHistory)
        {
        Console.WriteLine(
          $"Database version: {databasePath} Version={item.VersionNr} {item.Description}");
        }

      // Request persons when table is empty

      var emptyPersonList = PersonDataAccess.GetAllPersons();
      if (emptyPersonList.Count == 0)
        {
        Console.WriteLine("\r\nEmpty table returns zero records as expected, but list is not null");
        }

      var person1 = new PersonModel
        {
        FirstName = "Rudolf",
        LastName = "Heijink"
        };
      person1.Id = PersonDataAccess.InsertPerson(person1);

      var person2 = new PersonModel
        {
        FirstName = "Tim",
        LastName = "Corey"
        };
      person2.Id = PersonDataAccess.InsertPerson(person2);

      var personList = PersonDataAccess.GetAllPersons();
      foreach (var person in personList)
        {
        Console.WriteLine(
          $"All records:First name={person.FirstName} Last name= {person.LastName}");
        }

      var person3 = PersonDataAccess.GetPersonsById(2);
      Console.WriteLine(
        $"\r\nSingle record: First name={person3.FirstName} Last name= {person3.LastName}");

      person1.FirstName = "Rudolf Jan";
      PersonDataAccess.UpdatePerson(person1);
      var person4 = PersonDataAccess.GetPersonsById(person1.Id);

      Console.WriteLine(
        $"\r\nUpdated record: First name={person4.FirstName} Last name= {person4.LastName}");
      PersonDataAccess.DeletePerson(1);
      PersonDataAccess.DeletePerson(1); // try again, should not crash
      var person5 =
        PersonDataAccess
          .GetPersonsById(person1
            .Id); // attempt to request non-existent record, should return null?
      if (person5 == null)
        {
        Console.WriteLine("Requesting a deleted record results in null value");
        }

      // Last step is demo on how to proceed tu upgrade an existing database.
      DatabaseUpdater();

      // Check table structure
      var s = DbManager.GetTableInfo("Persons");
      foreach (var line in s)
        {
        Console.WriteLine($"{line.Id} {line.ColumnName} {line.ColumnType}");
        }
      }

    private static void DatabaseUpdater()
      {
      var currentVersion = DbManager.GetCurrentVersion();
      switch (currentVersion.VersionNr)
        {
        case 2:
            {
            UpdateToVersion3();
            goto case 3;
            }
        case 3:
            {
            UpdateToVersion4();
            break;
            }
        }
      }

    private static void UpdateToVersion4()
      {
      DatabaseFactory.CreateStructuresForVersion3();
      DbManager.UpdateDatabaseVersionNumber(4, "Phone number field added to personsTable");
      }

    private static void UpdateToVersion3()
      {
      DatabaseFactory.CreateStructuresForVersion4();
      DbManager.UpdateDatabaseVersionNumber(3, "Table profile added");
      }
    }
  }
