using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=hair_salon_test;";
    }

    [TestMethod]
    public void GetAll_GetAllStylistsAtFirst_0()
    {
      int actual = Stylist.GetAll().Count;
      Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void Save_SavesStylistToDataBase_StylistList()
    {
      Stylist newStylist = new Stylist("TestStylist", "www.images.com/stylistpicture.jpg");
      newStylist.Save();

      List<Stylist> expectedStylistList = new List<Stylist>{newStylist};

      List<Stylist> actualStylistList = Stylist.GetAll();

      CollectionAssert.AreEqual(expectedStylistList, actualStylistList);
    }

    [TestMethod]
    public void Find_FindsStylistByIdInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("TestStylist", "www.images.com/stylistpicture.jpg");
      testStylist.Save();

      Stylist expected = testStylist;
      Stylist actual = Stylist.Find(testStylist.GetId());

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Update_UpdatesStylistNameInDatabase_Stylist()
    {
      string name = "TestStylist";
      Stylist testStylist = new Stylist(name, "www.image.com/image.jpg");
      testStylist.Save();
      string newName = "TestStylistNewName";

      testStylist.Update(newName);

      string expected = newName;
      string actual = testStylist.GetName();

      Assert.AreEqual(expected, actual);
    }


    [TestMethod]
    public void Delete_DeleteStylistByIdInDatabase_StylistList()
    {
      Stylist stylist1 = new Stylist("TestStylistName", "www.image.com/image.jpg");
      Stylist stylist2 = new Stylist("TestStylistName2", "www.image.com/image2.jpg");
      stylist1.Save();
      stylist2.Save();

      List<Stylist> expected = new List<Stylist> {stylist2};
      stylist1.Delete();

      List<Stylist> actual = Stylist.GetAll();

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Search_SearchStylistByNameInDatabase_Stylist()
    {
      Stylist stylist1 = new Stylist("Jeffery", "www.image.com/image.jpg");
      Stylist stylist2 = new Stylist("Amanda", "www.image.com/image2.jpg");
      stylist1.Save();
      stylist2.Save();

      Stylist expected = stylist1;

      Stylist actual = Stylist.Search("Jeffery");

      Assert.AreEqual(expected, actual);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }

  }
}
