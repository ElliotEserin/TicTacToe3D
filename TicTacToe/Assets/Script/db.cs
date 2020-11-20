using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class db : MonoBehaviour {

    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;


    // Use this for initialization
    void Start () {
        conn = "URI=file:" + Application.dataPath + "/Plugins/Users.s3db"; //Path to database.
      

        Deletvalue(1);
        insertvalue("elliot", "ahmedm@gmail.com", "sss");
        Updatevalue("elliot", "elliot@gamil.com", "1st",2);
        readers();


        

    }


    private void readers()
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "SELECT * " + "FROM Usersinfo";// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string Email = reader.GetString(2);
                string Phone = reader.GetString(3);
                Debug.Log("value= " + id + "  name =" + name + "  Eamil =" + Email + "   Phone" + Phone);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
    }

    void insertvalue(string name, string email, string address)
        {
            using (dbconn = new SqliteConnection(conn))
            {
                SqliteCommand cmd = new SqliteCommand();

                // SQL String.
                String sql = "INSERT INTO Usersinfo(Name, Email, Address)   VALUES(@name,@email,@address)";

                cmd.CommandText = sql;
                cmd.Connection = (SqliteConnection)dbconn;

                // SQL paramters
                cmd.Parameters.Add(new SqliteParameter("@name", name));
                cmd.Parameters.Add(new SqliteParameter("@email", email));
                cmd.Parameters.Add(new SqliteParameter("@address", address));

                cmd.ExecuteNonQuery();
        }
        }

        void Deletvalue(int id)
        {
            using (dbconn = new SqliteConnection(conn))
            {
                SqliteCommand cmd = new SqliteCommand();

                string sql = "DELETE FROM Usersinfo WHERE ID=@id";// table name
                cmd.CommandText = sql;
                cmd.Connection = (SqliteConnection)dbconn;

                cmd.Parameters.Add(new SqliteParameter("@id", id));

                cmd.ExecuteNonQuery();
        }
        }

        void Updatevalue(string name, string email, string address, int id)
        {
            using (dbconn = new SqliteConnection(conn))
            {
                SqliteCommand cmd = new SqliteCommand();

                string sql = "UPDATE Usersinfo SET Name =@name, Email =@email, Address =@address WHERE ID =@id";// table name

                cmd.CommandText = sql;
                cmd.Connection = (SqliteConnection)dbconn;

                // SQL paramters
                cmd.Parameters.Add(new SqliteParameter("@name", name));
                cmd.Parameters.Add(new SqliteParameter("@email", email));
                cmd.Parameters.Add(new SqliteParameter("@address", address));
                cmd.Parameters.Add(new SqliteParameter("@id", id));

            cmd.ExecuteNonQuery();
        }
        }
    // Update is called once per frame
    void Update () {
		
	}


}
