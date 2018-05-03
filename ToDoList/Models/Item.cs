using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;

    public Item (string description, int id=0)
    //int id=0 defaults the int id value to 0 IF there is nothing passed through
    {
      _description = description;
      _id = id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";

           MySqlParameter description = new MySqlParameter();
           description.ParameterName = "@ItemDescription";
           description.Value = _description;
           cmd.Parameters.Add(description);

           cmd.ExecuteNonQuery();
           //Interacting with databases eithor: Modifying Data or Retrieving Information
           //ExecuteNonQuery modifies the database by saving
           _id = (int) cmd.LastInsertedId;
           //GATHERS data assigned IDs
           //Explicit Cast --> (int) converts cmd.LastInsertedId to accept long data type (64-bit data)

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
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

    // public void Save()
    // {
    //   _instances.Add(this);
    // }
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

      public override bool Equals(System.Object otherItem)
      //The override keyword indicates to the compiler that the method we define under it should replace the method of the same name built into C#
      //override is best practice for Equals because...hey...why not
      //Equals() accepts any type of object, we must declare its argument as "System.Object"
      {
        if (!(otherItem is Item))
        {
          return false;
        }
        else
        {
          Item newItem = (Item) otherItem;
          //when we change an object from one type to another, its called "TYPE CASTING"
          bool idEquality = (this.GetId() == newItem.GetId());
          bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
          return (idEquality && descriptionEquality);
        }
      }

      public static Item Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open()

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM 'items' WHERE id = @thisId;";
        //@thisId is the placeholder for the ID property of the Item we're seeking in the database

        MySqlParameter thisId = new MySqlParameter();
        //Create a MySqlParamter called thisId
        thisId.ParameterName = "@thisId";
        //Define ParameterName property as @thisId to match the SQL command
        thisId.Value = id;
        //Define Value property of thisId as id
        cmd.Parameters.Add(thisId);
        //Adds thisId to Parameters property of cmd

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        int itemId = 0;
        string itemDescription = "";
        //defined outside of while loop to ensure we don't hit unanticipated errors ( like not being able to define values)

        while (rdr.Read())
        //To initiate reading the database, we run a while loop
        {
          itemId = rdr.GetInt32(0);
          //corresponds to the index positions
          itemDescription = rdr.GetString(1);

        }

        Item foundItem = new Item(itemDescription, itemId);

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

        return foundItem;
      }

    // public static Item Find(int searchId)
    // {
    //  return _allItems[searchId-1];
    // }
  }
}
