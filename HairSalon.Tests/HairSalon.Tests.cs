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

    public void Dispose()
    {
      Stylist.DeleteAll();
    }

  }
}
