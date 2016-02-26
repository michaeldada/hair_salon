using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _description;
    private int _stylistId;

    public Client(string Description, int StylistId, int Id = 0)
    {
      _id = Id;
      _description = Description;
      _stylistId = StylistId;

    }

    public override bool Equals(System.Object otherClient)
     {
       if (!(otherClient is Client))
       {
         return false;
       }
       else
       {
         Client newClient = (Client) otherClient;
         bool idEquality = (this.GetId() == newClient.GetId());
         bool descriptionEquality = (this.GetDescription() == newClient.GetDescription());
         bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();

         return (idEquality && descriptionEquality && stylistEquality);
       }
     }

     public void Delete()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);

       SqlParameter clientIdParameter = new SqlParameter();
       clientIdParameter.ParameterName = "@ClientId";
       clientIdParameter.Value = this.GetId();

       cmd.Parameters.Add(clientIdParameter);
       cmd.ExecuteNonQuery();

       if (conn != null)
       {
         conn.Close();
       }
     }

     public void Update(string newDescription)
     {
       SqlConnection conn = DB.Connection();
       SqlDataReader rdr;
       conn.Open();

       SqlCommand cmd = new SqlCommand("UPDATE clients SET description = @NewDescription OUTPUT INSERTED.description WHERE id = @ClientId;", conn);

       SqlParameter newDescriptionParameter = new SqlParameter();
       newDescriptionParameter.ParameterName = "@NewDescription";
       newDescriptionParameter.Value = newDescription;
       cmd.Parameters.Add(newDescriptionParameter);


       SqlParameter clientIdParameter = new SqlParameter();
       clientIdParameter.ParameterName = "@ClientId";
       clientIdParameter.Value = this.GetId();
       cmd.Parameters.Add(clientIdParameter);
       rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         this._description = rdr.GetString(0);
       }

       if (rdr != null)
       {
         rdr.Close();
       }

       if (conn != null)
       {
         conn.Close();
       }
     }

     public int GetStylistId()
     {
       return _stylistId;
     }
     public void SetStylistId(int newStylistId)
     {
       _stylistId = newStylistId;
     }
    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantDescription = rdr.GetString(1);
        int restaurantStylistId = rdr.GetInt32(2);

        Client newClient = new Client(restaurantDescription, restaurantStylistId, restaurantId);
        allClients.Add(newClient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allClients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (description, stylist_id) OUTPUT INSERTED.id VALUES (@ClientDescription, @ClientStylistId);", conn);

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@ClientDescription";
      descriptionParameter.Value = this.GetDescription();
      cmd.Parameters.Add(descriptionParameter);

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@ClientStylistId";
      stylistIdParameter.Value = this.GetStylistId();
      cmd.Parameters.Add(stylistIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientIdParameter);
      rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientDescription = null;
      int foundClientStylistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientDescription = rdr.GetString(1);
        foundClientStylistId = rdr.GetInt32(2);

      }
      Client foundClient = new Client(foundClientDescription, foundClientStylistId, foundClientId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundClient;
    }

    public static Client FindName(string Description )
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE description = @ClientDescription;", conn);
      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@ClientDescription";
      descriptionParameter.Value = Description;
      cmd.Parameters.Add(descriptionParameter);
      rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientDescription = null;
      int foundClientStylistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientDescription = rdr.GetString(1);
        foundClientStylistId = rdr.GetInt32(2);

      }
      Client foundClient = new Client(foundClientDescription, foundClientStylistId, foundClientId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundClient;
    }




    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
