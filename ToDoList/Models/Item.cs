using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    // private static List<Item> _instances = new List<Item> {};
    //omitted because we're now using database to store and pull information


    public Item (string description)
    {
      _description = description;
    }
    public Item (string description, int id)
    {
      _description = description;
      _id = id;
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
       //empty list which we will place information from the database
       MySqlConnection conn = DB.Connection();
       //passes conn to --> database connection
       conn.Open();
       //open database
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       //passes cmd --> MySqlCommand for...
       cmd.CommandText = @"SELECT * FROM items;";
       //showing all items from database
       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
       //passes rdr --> MySqlCommand to read SQL database
       while(rdr.Read())
       //rdr.Read() method sends the SQL Commands to the database and collects whatever the database returns in response to those commands
       //While looop will take each row of data returned from the database and perform actions with it
       {
         int itemId = rdr.GetInt32(0);
         //will receive the first column of data, where our Item's id values are stored (index 0)
         string itemDescription = rdr.GetString(1);
         //will receive the second column data, where our Item descriptions are stored (index 1)
         Item newItem = new Item(itemDescription, itemId);
         //instantiate new Item with receieved paramaters
         allItems.Add(newItem);
         //Add instantiated Item into allItems list
       }
       conn.Close();
       //after closing connection we manually confirm whether the connection exists...
       if (conn != null)
       //...If the connection DOES still exist...
       {
           conn.Dispose();
           //...fully clear the connection from memory
       }
       return allItems;
       //otherwise show all items from database
     }

    public void Save()
    {
      _instances.Add(this);
    }
    public static void ClearAll()
    {
       MySqlConnection conn = DB.Connection();
       //Creates conn object representing our connection to the database
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
      }

    public static Item Find(int searchId)
    {
     return _instances[searchId-1];
    }
  }
}
