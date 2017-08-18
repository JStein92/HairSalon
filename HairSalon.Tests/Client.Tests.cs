using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=hair_salon_test;";
    }

    [TestMethod]
    public void GetAll_GetAllClientsAtFirst_0()
    {
      int actual = Client.GetAll().Count;
      Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void Save_SavesClientToDataBase_ClientList()
    {
      Client newClient = new Client("TestClient", "333-333-3333", 0);
      newClient.Save();

      List<Client> expectedClientList = new List<Client>{newClient};

      List<Client> actualClientList = Client.GetAll();

      CollectionAssert.AreEqual(expectedClientList, actualClientList);
    }

    [TestMethod]
    public void Find_FindsClientByIdInDatabase_Client()
    {
    Client newClient = new Client("TestClient", "333-333-3333", 0);
      newClient.Save();

      Client expected = newClient;
      Client actual = Client.Find(newClient.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Update_UpdatesClientNameInDatabase_Client()
    {
      string name = "TestClient";
      Client newClient = new Client(name, "333-333-3333", 0);
      newClient.Save();
      string newName = "TestClientNewName";

      newClient.Update(newName);

      string expected = newName;
      string actual = newClient.GetName();

      Assert.AreEqual(expected, actual);
    }


    [TestMethod]
    public void Delete_DeleteClientByIdInDatabase_ClientList()
    {
      Client client1 = new Client("TestClientName", "333-333-3333", 0);
      Client client2 = new Client("TestClientName2", "333-333-3334");
      client1.Save();
      client2.Save();

      List<Client> expected = new List<Client> {client2};
      client1.Delete();

      List<Client> actual = Client.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Search_SearchClientByNameInDatabase_Client()
    {
      Client client1 = new Client("Jeffery", "333-333-3333");
      Client client2 = new Client("Amanda", "333-333-3334");
      client1.Save();
      client2.Save();

      Client expected = client1;

      Client actual = Client.Search("Jeffery");

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void UpdateStylist_UpdateStylistForClient_Client()
    {
      Stylist stylist1 = new Stylist("TestStylistName", "www.image.com/image.jpg");
      Stylist stylist2 = new Stylist("TestStylistName2", "www.image.com/image2.jpg");
      stylist1.Save();
      stylist2.Save();

      Client client1 = new Client("Jeffery", "333-333-3333", stylist1.GetId());
      Client client2 = new Client("Amanda", "333-333-3334", stylist2.GetId());
      client1.Save();
      client2.Save();

      client1.UpdateStylist(stylist2.GetId());

      Stylist expected = stylist2;

      Stylist actual = Stylist.Find(client1.GetStylist());

      Console.WriteLine(Stylist.Find(client1.GetStylist()).GetName());

      Assert.AreEqual(expected, actual);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }

  }
}
