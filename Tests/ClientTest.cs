using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    public void Dispose()
   {
     Client.DeleteAll();
   }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Client.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {

      Client firstClient = new Client("Bonnie", 1);
      Client secondClient = new Client("Bonnie", 1);

      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      Client testClient = new Client("Bonnie", 1);

      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Client testClient = new Client("Bonnie", 1);

      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Update_UpdatesClientInDatabase()
    {
      //Arrange
      string name = "Mindy";
      Client testClient = new Client(name, 1);
      testClient.Save();
      string newName = "Cindy";

      //Act
      testClient.Update(newName);

      string result = testClient.GetDescription();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesClientFromDatabase()
    {
      //Arrange
      Client testClient1 = new Client("Bonnie", 1);
      testClient1.Save();
      Client testClient2 = new Client("Jody", 2);
      testClient2.Save();

      //Act
      testClient1.Delete();
      List<Client> resultClients = Client.GetAll();
      List<Client> testClientList = new List<Client> {testClient2};

      //Assert
      Assert.Equal(testClientList, resultClients);
    }


    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      Client testClient = new Client("Bonnie", 1);
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());

      Assert.Equal(testClient, foundClient);
    }

  }
}
