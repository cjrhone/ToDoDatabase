using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;


namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    private static List<Item> _instances = new List<Item> {};


    public Item (string description)
    {
      _description = description;
      _instances.Add(this);
      _id = _instances.Count;
    }
    public string GetDescription()
    {
      return _description;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public static List<Item> GetAll()
     {
       List<Item> allItems = new List<Item> {};
      //  MySqlConnection conn = DB.Connection();
      //  conn.Open();
      //  MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      //  cmd.CommandText = @"SELECT * FROM items;";
      //  MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      //  while(rdr.Read())
      //  {
      //    int itemId = rdr.GetInt32(0);
      //    string itemDescription = rdr.GetString(1);
      //    Item newItem = new Item(itemDescription, itemId);
      //    allItems.Add(newItem);
      //  }
      //  conn.Close();
      //  if (conn != null)
      //  {
      //      conn.Dispose();
      //  }
       return allItems;
     }
      // return _instances;
    public void Save()
    {
      _instances.Add(this);
    }
    public static void ClearAll()
    {
       MySqlConnection conn = DB.Connection();
       //Creates conn object represting our connection to the database
       conn.Open();
       //manually opens the connection ( conn ) with conn.Open()

       var cmd = conn.CreateCommand() as MySqlCommand;
       //Define cmd as --> creating command --> MySqlCommand... then...
       cmd.CommandText = @"DELETE FROM items;";
       //...Define CommandText property using SQL statement, which will clear the items table in our database
       cmd.ExecuteNonQuery();
       //Executes SQL statements that modify data (like deletion)

       conn.Close();
       //Finally, we make sure to close our connection with Close()...
       if (conn != null)
       {
           conn.Dispose();
       }
       //...including an if statement that disposes of the connection if it's not null.
       //Testing the commit, delete this comment later ,jahsjdhsajdksahdkjsahdkajs
       //test
      }

    public static Item Find(int searchId)
    {
     return _instances[searchId-1];
    }
  }
}
