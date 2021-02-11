using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class CategoryDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal List<CATEGORY> GetAll(bool? state)
        {
            List<CATEGORY> categories = new List<CATEGORY>();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = "";
                if (state == null)
                    sqlQuery = $@"SELECT TCATEGORY.*, TSTATE.NAME AS STATE_NAME FROM TCATEGORY INNER JOIN TSTATE ON TSTATE.ID = TCATEGORY.STATE_ID";
                else
                    sqlQuery = $@"SELECT TCATEGORY.*, TSTATE.NAME AS STATE_NAME FROM TCATEGORY INNER JOIN TSTATE ON TSTATE.ID = TCATEGORY.STATE_ID WHERE TCATEGORY.STATE_ID = @STATE_ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (state != null)
                        cmd.Parameters.AddWithValue("@STATE_ID", (bool)state ? 1 : 2);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(new CATEGORY()
                        {
                            ID = (int)reader["ID"],
                            NAME = (string)reader["NAME"],
                            STATE_ID = (int)reader["STATE_ID"],
                            STATE_NAME = (string)reader["STATE_NAME"]
                        });
                    }
                    con.Close();
                }
            }

            return categories;
        }

        internal CATEGORY GetById(int id)
        {
            CATEGORY category = new CATEGORY();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"SELECT TCATEGORY.*, TSTATE.NAME AS STATE_NAME FROM TCATEGORY INNER JOIN TSTATE ON TSTATE.ID = TCATEGORY.STATE_ID WHERE TCATEGORY.ID=@ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        category.ID = (int)reader["ID"];
                        category.NAME = (string)reader["NAME"];
                        category.STATE_ID = (int)reader["STATE_ID"];
                        category.STATE_NAME = (string)reader["STATE_NAME"]; 
                    }
                    con.Close();
                }
            }

            return category;
        }

        internal void Update(CATEGORY category)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"UPDATE TCATEGORY SET NAME = @NAME, STATE_ID = @STATE_ID WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@NAME", category.NAME);
                    cmd.Parameters.AddWithValue("@STATE_ID", category.STATE_ID);
                    cmd.Parameters.AddWithValue("@ID", category.ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Delete(int id)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"DELETE FROM TCATEGORY WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Insert(CATEGORY category)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"INSERT INTO TCATEGORY (NAME, STATE_ID) VALUES (@NAME, @STATE_ID)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@NAME", category.NAME);
                    cmd.Parameters.AddWithValue("@STATE_ID", category.STATE_ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}