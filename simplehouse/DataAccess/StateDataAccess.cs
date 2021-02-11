using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class StateDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal List<STATE> GetAll()
        {
            List<STATE> states = new List<STATE>();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"SELECT * FROM TSTATE";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        states.Add(new STATE()
                        {
                            ID = (int)reader["ID"],
                            NAME = (string)reader["NAME"]
                        });
                    }
                    con.Close();
                }
            }

            return states;
        }
    }
}