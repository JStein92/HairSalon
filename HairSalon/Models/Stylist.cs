using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _image;

    public Stylist(string name, string image, int id=0)
    {
      _id = id;
      _name = name;
      _image = image;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetImage()
    {
      return _image;
    }


    public static List<Stylist> GetAll()
    {
      List<Stylist> stylistList = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistImage = rdr.GetString(2);

        Stylist newStylist = new Stylist(stylistName, stylistImage, stylistId);
        stylistList.Add(newStylist);
      }
      conn.Close();
      return stylistList;
    }
  }
}
