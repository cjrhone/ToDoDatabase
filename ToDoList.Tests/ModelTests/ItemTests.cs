using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System;

namespace ToDoList.Tests
{

    [TestClass]
    public class ItemTests : IDisposable
    {

      public void Dispose()
      {
        Item.ClearAll();
      }

      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //Act
        int result = Item.GetAll().Count;

        //Assert
        Assert.AreEqual(0, result);
      }

      public ItemTests()
      {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        //connects to our "todo_test" database
      }
    }
}
