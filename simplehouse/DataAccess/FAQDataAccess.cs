using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class FAQDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal List<FAQ> GetAll(bool? state)
        {
            List<FAQ> faqs = new List<FAQ>();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT TFAQ.*, TSTATE.NAME AS STATE_NAME FROM TFAQ INNER JOIN TSTATE ON TSTATE.ID = TFAQ.STATE_ID";
                if (state != null)
                    sqlQuery += $@" WHERE TFAQ.STATE_ID = @STATE_ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (state != null)
                        cmd.Parameters.AddWithValue("@STATE_ID", (bool)state ? 1 : 2);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        faqs.Add(new FAQ()
                        {
                            ID = (int)reader["ID"],
                            TITLE = (string)reader["TITLE"],
                            DETAILS = (string)reader["DETAILS"],
                            STATE_ID = (int)reader["STATE_ID"],
                            STATE_NAME = (string)reader["STATE_NAME"]
                        });
                    }
                    con.Close();
                }
            }

            return faqs;
        }

        internal FAQ GetById(int id)
        {
            FAQ faq = new FAQ();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"SELECT TFAQ.*, TSTATE.NAME AS STATE_NAME FROM TFAQ INNER JOIN TSTATE ON TSTATE.ID = TFAQ.STATE_ID WHERE TFAQ.ID=@ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        faq.ID = (int)reader["ID"];
                        faq.TITLE = (string)reader["TITLE"];
                        faq.DETAILS = (string)reader["DETAILS"];
                        faq.STATE_ID = (int)reader["STATE_ID"];
                        faq.STATE_NAME = (string)reader["STATE_NAME"];
                    }
                    con.Close();
                }
            }

            return faq;
        }

        internal void Insert(FAQ faq)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"INSERT INTO TFAQ (TITLE, DETAILS, STATE_ID) VALUES (@TITLE, @DETAILS ,@STATE_ID)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@TITLE", faq.TITLE);
                    cmd.Parameters.AddWithValue("@DETAILS", faq.DETAILS);
                    cmd.Parameters.AddWithValue("@STATE_ID", faq.STATE_ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Update(FAQ faq)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"UPDATE TFAQ SET TITLE = @TITLE, DETAILS = @DETAILS, STATE_ID = @STATE_ID WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@TITLE", faq.TITLE);
                    cmd.Parameters.AddWithValue("@DETAILS", faq.DETAILS);
                    cmd.Parameters.AddWithValue("@STATE_ID", faq.STATE_ID);
                    cmd.Parameters.AddWithValue("@ID", faq.ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Delete(int id)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"DELETE FROM TFAQ WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}