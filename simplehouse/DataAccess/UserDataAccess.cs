using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class UserDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal bool IsLoginOK(USER user)
        {
            bool isOK = false;
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"SELECT * FROM TUSER WHERE EMAIL=@EMAIL AND PASSWORD=@PASSWORD";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@EMAIL", user.EMAIL);
                    cmd.Parameters.AddWithValue("@PASSWORD", user.PASSWORD);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        isOK = true;

                    con.Close();
                }
            }

            return isOK;
        }

        internal USER GetByEmail(string email)
        {
            USER user = new USER();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"SELECT * FROM TUSER WHERE EMAIL=@EMAIL";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@EMAIL", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.ID = (int)reader["ID"];
                        user.NAME = (string)reader["NAME"];
                        user.EMAIL = (string)reader["EMAIL"];
                        user.PASSWORD = (string)reader["PASSWORD"];
                    }
                    con.Close();
                }
            }

            return user;
        }

        internal USER GetById(int userId)
        {
            USER user = new USER();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"SELECT * FROM TUSER WHERE ID=@ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.ID = (int)reader["ID"];
                        user.NAME = (string)reader["NAME"];
                        user.EMAIL = (string)reader["EMAIL"];
                        user.PASSWORD = (string)reader["PASSWORD"];
                    }
                    con.Close();
                }
            }

            return user;
        }

        internal void ChangePassword(int userId, string newPassword)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"UPDATE TUSER SET PASSWORD = @PASSWORD WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@PASSWORD", newPassword);
                    cmd.Parameters.AddWithValue("@ID", userId);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}