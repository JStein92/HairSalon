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

    public void Dispose()
    {
      //Stylist.DeleteAll();
    }

  }
}
