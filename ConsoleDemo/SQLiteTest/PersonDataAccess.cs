using ConsoleDemo.SQLiteTest.Models;
using SQLiteDatabase.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDemo.SQLiteTest
  {
  public class PersonDataAccess
    {
    public static List<PersonModel> GetAllPersons()
      {
      var sql = "SELECT * FROM Persons";
      return DbAccess.LoadData<PersonModel, dynamic>(sql, new { });
      }

    public static PersonModel GetPersonsById(int id)
      {
      var sql = "SELECT * FROM Persons WHERE Id=@id";
      return DbAccess.LoadData<PersonModel, dynamic>(sql, new {id }).FirstOrDefault();
      }

    public static int InsertPerson(PersonModel person)
      {
      var sql = $"INSERT OR IGNORE INTO Persons (FirstName,LastName) VALUES(@FirstName, @LastName);{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new {person.FirstName, person.LastName});
      }

    public static int UpdatePerson(PersonModel person)
      {
      var sql = $"UPDATE OR IGNORE Persons SET FirstName=@FirstName, LastName=@LastName WHERE Id= @Id;{DbAccess.LastRowInsertQuery}";
      return DbAccess.SaveData<dynamic>(sql, new { person.FirstName, person.LastName, person.Id });
      }

    public static void DeletePerson(int id)
      {
      var sql = "DELETE FROM Persons WHERE Id=@id";
      DbAccess.SaveData<dynamic>(sql, new { id });
      }
    }
  }
