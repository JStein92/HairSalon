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

    public override bool Equals(System.Object otherObject)
    {
      if (!(otherObject is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherObject;
        return this.GetId().Equals(newStylist.GetId());
      }
    }

    public override int GetHashCode()
    {
        return this.GetId().GetHashCode();
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (id,name,image) VALUES (@thisId, @name, @image);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@thisId";
      id.Value = _id;
      cmd.Parameters.Add(id);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = _name;
      cmd.Parameters.Add(name);

      MySqlParameter image = new MySqlParameter();
      image.ParameterName = "@image";
      image.Value = _image;
      cmd.Parameters.Add(image);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();

    }

    public static Stylist Find(int id)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@thisId);";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@thisId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int stylistId = 0;
     string stylistName = "";
     string stylistImage = "";

     while(rdr.Read())
     {
       stylistId = rdr.GetInt32(0);
       stylistName = rdr.GetString(1);
       stylistImage = rdr.GetString(2);
     }
     Stylist foundStylist = new Stylist(stylistName, stylistImage, stylistId);
     conn.Close();
     return foundStylist;
  }


    public void Update(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      conn.Close();
      _name = newName;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @thisId;";

      MySqlParameter searchId2 = new MySqlParameter();
      searchId2.ParameterName = "@thisId";
      searchId2.Value = _id;
      cmd.Parameters.Add(searchId2);

      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public static Stylist Search(string searchName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE name = (@searchName);";

      MySqlParameter searchNameParameter = new MySqlParameter();
      searchNameParameter.ParameterName = "@searchName";
      searchNameParameter.Value = searchName;
      cmd.Parameters.Add(searchNameParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int stylistId = 0;
      string stylistName = "";
      string stylistImage = "";

      while(rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
        stylistImage = rdr.GetString(2);
      }

      Stylist foundStylist = new Stylist(stylistName, stylistImage, stylistId);
      conn.Close();
      return foundStylist;
    }

    public List<Client> GetAllClients()
    {
      List<Client> clientList = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @thisId;";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@thisId";
      stylist_id.Value = _id;
      cmd.Parameters.Add(stylist_id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientPhone = rdr.GetString(2);
        int stylistId = rdr.GetInt32(3);

        Client newClient = new Client(clientName, clientPhone, stylistId, clientId);
        clientList.Add(newClient);
      }
      conn.Close();
      return clientList;
    }


    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();

      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
