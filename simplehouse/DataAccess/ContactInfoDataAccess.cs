using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class ContactInfoDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal CONTACTINFO Get()
        {
            CONTACTINFO contactInfo = new CONTACTINFO();
            using (con = new SqlConnection(connectionString))
            {
                string checkSql = @"SELECT * FROM TCONTACTINFO";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, con))
                {
                    con.Open();
                    SqlDataReader checkReader = checkCmd.ExecuteReader();
                    if (checkReader.Read())
                    {
                        contactInfo = new CONTACTINFO()
                        {
                            ID = (int)checkReader["ID"],
                            EMAIL = (string)checkReader["EMAIL"],
                            PHONE = (string)checkReader["PHONE"],
                            ADDRESS = (string)checkReader["ADDRESS"],
                            FACEBOOK = (string)checkReader["FACEBOOK"],
                            INSTAGRAM = (string)checkReader["INSTAGRAM"],
                            TWITTER = (string)checkReader["TWITTER"]
                        };
                    }
                }
            }

            return contactInfo;
        }

        internal void Update(CONTACTINFO contactInfo)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"UPDATE TCONTACTINFO SET ADDRESS=@ADDRESS, PHONE=@PHONE, EMAIL=@EMAIL, FACEBOOK=@FACEBOOK, TWITTER=@TWITTER, INSTAGRAM=@INSTAGRAM WHERE ID=@ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@PHONE", contactInfo.PHONE);
                    cmd.Parameters.AddWithValue("@EMAIL", contactInfo.EMAIL);
                    cmd.Parameters.AddWithValue("@ADDRESS", contactInfo.ADDRESS);
                    cmd.Parameters.AddWithValue("@TWITTER", contactInfo.TWITTER);
                    cmd.Parameters.AddWithValue("@INSTAGRAM", contactInfo.INSTAGRAM);
                    cmd.Parameters.AddWithValue("@FACEBOOK", contactInfo.FACEBOOK);
                    cmd.Parameters.AddWithValue("@ID", contactInfo.ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Insert(CONTACTINFO contactInfo)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"INSERT INTO TCONTACTINFO (ADDRESS, PHONE, EMAIL, FACEBOOK, TWITTER, INSTAGRAM) VALUES 
                                                       (@ADDRESS, @PHONE, @EMAIL, @FACEBOOK, @TWITTER, @INSTAGRAM)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@PHONE", contactInfo.PHONE);
                    cmd.Parameters.AddWithValue("@EMAIL", contactInfo.EMAIL);
                    cmd.Parameters.AddWithValue("@ADDRESS", contactInfo.ADDRESS);
                    cmd.Parameters.AddWithValue("@TWITTER", contactInfo.TWITTER);
                    cmd.Parameters.AddWithValue("@INSTAGRAM", contactInfo.INSTAGRAM);
                    cmd.Parameters.AddWithValue("@FACEBOOK", contactInfo.FACEBOOK);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}