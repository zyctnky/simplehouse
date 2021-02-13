using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class ContactFormDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal List<CONTACTFORM> GetAll()
        {
            List<CONTACTFORM> contactForms = new List<CONTACTFORM>();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM TCONTACTFORM ORDER BY SEND_DATE DESC";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        contactForms.Add(new CONTACTFORM()
                        {
                            ID = (int)reader["ID"],
                            NAME = (string)reader["NAME"],
                            EMAIL = (string)reader["EMAIL"],
                            MESSAGE = (string)reader["MESSAGE"],
                            SEND_DATE = (DateTime)reader["SEND_DATE"]
                        });
                    }
                    con.Close();
                }
            }

            return contactForms;
        }

        internal void Insert(CONTACTFORM contactForm)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"INSERT INTO TCONTACTFORM (NAME, EMAIL, MESSAGE, SEND_DATE) VALUES (@NAME, @EMAIL, @MESSAGE, @SEND_DATE)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@NAME", contactForm.NAME);
                    cmd.Parameters.AddWithValue("@EMAIL", contactForm.EMAIL);
                    cmd.Parameters.AddWithValue("@MESSAGE", contactForm.MESSAGE);
                    cmd.Parameters.AddWithValue("@SEND_DATE", DateTime.Now);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal CONTACTFORM GetById(int id)
        {
            CONTACTFORM contactForm = new CONTACTFORM();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM TCONTACTFORM WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        contactForm = new CONTACTFORM()
                        {
                            ID = (int)reader["ID"],
                            NAME = (string)reader["NAME"],
                            EMAIL = (string)reader["EMAIL"],
                            MESSAGE = (string)reader["MESSAGE"],
                            SEND_DATE = (DateTime)reader["SEND_DATE"]
                        };
                    }
                    con.Close();
                }
            }

            return contactForm;
        }

        internal void Delete(int id)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"DELETE FROM TCONTACTFORM WHERE ID = @ID";

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