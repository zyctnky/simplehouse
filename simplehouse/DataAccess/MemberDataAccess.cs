using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class MemberDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal List<MEMBER> GetAll(bool? state)
        {
            List<MEMBER> members = new List<MEMBER>();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"SELECT TMEMBER.*, TSTATE.NAME AS STATE_NAME FROM TMEMBER INNER JOIN TSTATE ON TSTATE.ID = TMEMBER.STATE_ID";
                if (state != null)
                    sqlQuery += $@" WHERE TMEMBER.STATE_ID = @STATE_ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (state != null)
                        cmd.Parameters.AddWithValue("@STATE_ID", (bool)state ? 1 : 2);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        members.Add(new MEMBER()
                        {
                            ID = (int)reader["ID"],
                            NAME = (string)reader["NAME"],
                            TITLE = (string)reader["TITLE"],
                            DESCRIPTION = (string)reader["DESCRIPTION"],
                            IMAGE = (string)reader["IMAGE"],
                            TWITTER = (string)reader["TWITTER"],
                            INSTAGRAM = (string)reader["INSTAGRAM"],
                            FACEBOOK = (string)reader["FACEBOOK"],
                            STATE_ID = (int)reader["STATE_ID"],
                            STATE_NAME = (string)reader["STATE_NAME"]
                        });
                    }
                    con.Close();
                }
            }

            return members;
        }

        internal MEMBER GetById(int id)
        {
            MEMBER member = new MEMBER();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"SELECT TMEMBER.*, TSTATE.NAME AS STATE_NAME FROM TMEMBER INNER JOIN TSTATE ON TSTATE.ID = TMEMBER.STATE_ID 
                                    WHERE TMEMBER.ID=@ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        member.ID = (int)reader["ID"];
                        member.NAME = (string)reader["NAME"];
                        member.TITLE = (string)reader["TITLE"];
                        member.DESCRIPTION = (string)reader["DESCRIPTION"];
                        member.IMAGE = (string)reader["IMAGE"];
                        member.TWITTER = (string)reader["TWITTER"];
                        member.INSTAGRAM = (string)reader["INSTAGRAM"];
                        member.FACEBOOK = (string)reader["FACEBOOK"];
                        member.STATE_ID = (int)reader["STATE_ID"];
                        member.STATE_NAME = (string)reader["STATE_NAME"];
                    }
                    con.Close();
                }
            }

            return member;
        }

        internal void Insert(MEMBER member)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"INSERT INTO TMEMBER (NAME, TITLE, DESCRIPTION, IMAGE, TWITTER, INSTAGRAM, FACEBOOK, STATE_ID) VALUES 
                                                       (@NAME, @TITLE, @DESCRIPTION, @IMAGE, @TWITTER, @INSTAGRAM, @FACEBOOK, @STATE_ID)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@NAME", member.NAME);
                    cmd.Parameters.AddWithValue("@TITLE", member.TITLE);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", member.DESCRIPTION);
                    cmd.Parameters.AddWithValue("@IMAGE", member.IMAGE);
                    cmd.Parameters.AddWithValue("@TWITTER", member.TWITTER);
                    cmd.Parameters.AddWithValue("@INSTAGRAM", member.INSTAGRAM);
                    cmd.Parameters.AddWithValue("@FACEBOOK", member.FACEBOOK);
                    cmd.Parameters.AddWithValue("@STATE_ID", member.STATE_ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Update(MEMBER member)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"UPDATE TMEMBER SET NAME=@NAME, TITLE=@TITLE, DESCRIPTION=@DESCRIPTION, IMAGE=@IMAGE, TWITTER=@TWITTER, INSTAGRAM=@INSTAGRAM, FACEBOOK=@FACEBOOK, STATE_ID=@STATE_ID WHERE TMEMBER.ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@NAME", member.NAME);
                    cmd.Parameters.AddWithValue("@TITLE", member.TITLE);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", member.DESCRIPTION);
                    cmd.Parameters.AddWithValue("@IMAGE", member.IMAGE);
                    cmd.Parameters.AddWithValue("@TWITTER", member.TWITTER);
                    cmd.Parameters.AddWithValue("@INSTAGRAM", member.INSTAGRAM);
                    cmd.Parameters.AddWithValue("@FACEBOOK", member.FACEBOOK);
                    cmd.Parameters.AddWithValue("@STATE_ID", member.STATE_ID);
                    cmd.Parameters.AddWithValue("@ID", member.ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Delete(int id)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"DELETE FROM TMEMBER WHERE ID = @ID";

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