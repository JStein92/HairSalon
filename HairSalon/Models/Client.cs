using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _phone;
    private int _stylistId;

    public Client(string name, string phone, int stylistId = 0, int id=0)
    {
      _id = id;
      _name = name;
      _phone = phone;
      _stylistId = stylistId;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetPhone()
    {
      return _phone;
    }
    public int GetStylist()
    {
      return _stylistId;
    }

    public override bool Equals(System.Object otherObject)
    {
      if (!(otherObject is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherObject;
        return this.GetId().Equals(newClient.GetId());
      }
    }

    public override int GetHashCode()
    {
        return this.GetId().GetHashCode();
    }

    public Stylist FindStylist()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@thisId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _stylistId;
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


    public static List<Client> GetAll()
    {
      List<Client> clientList = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (id,name,phone,stylist_id) VALUES (@thisId, @name, @phone, @stylistId);";

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@thisId";
      id.Value = _id;
      cmd.Parameters.Add(id);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = _name;
      cmd.Parameters.Add(name);

      MySqlParameter phone = new MySqlParameter();
      phone.ParameterName = "@phone";
      phone.Value = _phone;
      cmd.Parameters.Add(phone);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = _stylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();

    }

    public static Client Find(int id)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients WHERE id = (@thisId);";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@thisId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int clientId = 0;
     string clientName = "";
     string clientPhone = "";
     int stylistId = 0;

     while(rdr.Read())
     {
       clientId = rdr.GetInt32(0);
       clientName = rdr.GetString(1);
       clientPhone = rdr.GetString(2);
       stylistId = rdr.GetInt32(3);
     }
     Client foundClient = new Client(clientName, clientPhone, stylistId, clientId);
     conn.Close();
     return foundClient;
  }


    public void Update(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @thisId;";

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

    public void UpdateStylist(int newStylistId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET stylist_id = @newStylistId WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@newStylistId";
      stylistId.Value = newStylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      conn.Close();
      _stylistId = newStylistId;
    }
    public void UpdatePhone(string newPhone)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET phone = @newPhone WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter clientPhone = new MySqlParameter();
      clientPhone.ParameterName = "@newPhone";
      clientPhone.Value = newPhone;
      cmd.Parameters.Add(clientPhone);

      cmd.ExecuteNonQuery();
      conn.Close();
      _phone = newPhone;
    }
    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter clientName = new MySqlParameter();
      clientName.ParameterName = "@newName";
      clientName.Value = newName;
      cmd.Parameters.Add(clientName);

      cmd.ExecuteNonQuery();
      conn.Close();
      _name = newName;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public static void DeleteById(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public static Client Search(string searchName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE name = (@searchName);";

      MySqlParameter searchNameParameter = new MySqlParameter();
      searchNameParameter.ParameterName = "@searchName";
      searchNameParameter.Value = searchName;
      cmd.Parameters.Add(searchNameParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int clientId = 0;
      string clientName = "";
      string clientPhone = "";
      int stylistId = 0;

      while(rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientPhone = rdr.GetString(2);
        stylistId = rdr.GetInt32(3);
      }

      Client foundClient = new Client(clientName, clientPhone, stylistId, clientId);
      conn.Close();
      return foundClient;
    }


    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();

    }

  }
}
