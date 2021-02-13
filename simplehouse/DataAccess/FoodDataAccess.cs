using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace simplehouse.DataAccess
{
    public class FoodDataAccess
    {
        SqlConnection con;
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        internal List<FOOD> GetAll(bool? state)
        {
            List<FOOD> foods = new List<FOOD>();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"SELECT TFOOD.*, TCATEGORY.NAME AS CATEGORY_NAME, TSTATE.NAME AS STATE_NAME FROM TFOOD
                                    INNER JOIN TCATEGORY ON TCATEGORY.ID = TFOOD.CATEGORY_ID
                                    INNER JOIN TSTATE ON TSTATE.ID = TFOOD.STATE_ID";
                if (state != null)
                    sqlQuery += $@" WHERE TFOOD.STATE_ID = @STATE_ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (state != null)
                        cmd.Parameters.AddWithValue("@STATE_ID", (bool)state ? 1 : 2);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        foods.Add(new FOOD()
                        {
                            ID = (int)reader["ID"],
                            TITLE = (string)reader["TITLE"],
                            DESCRIPTION = (string)reader["DESCRIPTION"],
                            PRICE = (decimal)reader["PRICE"],
                            IMAGE = (string)reader["IMAGE"],
                            CATEGORY_ID = (int)reader["CATEGORY_ID"],
                            CATEGORY_NAME = (string)reader["CATEGORY_NAME"],
                            STATE_ID = (int)reader["STATE_ID"],
                            STATE_NAME = (string)reader["STATE_NAME"]
                        });
                    }
                    con.Close();
                }
            }

            return foods;
        }

        internal List<FOOD> GetByCategoryId(int categoryId, bool? state)
        {
            List<FOOD> foods = new List<FOOD>();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"SELECT TFOOD.*, TCATEGORY.NAME AS CATEGORY_NAME, TSTATE.NAME AS STATE_NAME FROM TFOOD
                                    INNER JOIN TCATEGORY ON TCATEGORY.ID = TFOOD.CATEGORY_ID
                                    INNER JOIN TSTATE ON TSTATE.ID = TFOOD.STATE_ID WHERE TFOOD.CATEGORY_ID = @CATEGORY_ID ";

                if (state != null)
                    sqlQuery += $@" AND TFOOD.STATE_ID = @STATE_ID";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryId);
                    if (state != null)
                        cmd.Parameters.AddWithValue("@STATE_ID", (bool)state ? 1 : 2);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        foods.Add(new FOOD()
                        {
                            ID = (int)reader["ID"],
                            TITLE = (string)reader["TITLE"],
                            DESCRIPTION = (string)reader["DESCRIPTION"],
                            PRICE = (decimal)reader["PRICE"],
                            IMAGE = (string)reader["IMAGE"],
                            CATEGORY_ID = (int)reader["CATEGORY_ID"],
                            CATEGORY_NAME = (string)reader["CATEGORY_NAME"],
                            STATE_ID = (int)reader["STATE_ID"],
                            STATE_NAME = (string)reader["STATE_NAME"]
                        });
                    }
                    con.Close();
                }
            }

            return foods;
        }

        internal FOOD GetById(int id)
        {
            FOOD food = new FOOD();
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"SELECT TFOOD.*, TCATEGORY.NAME AS CATEGORY_NAME, TSTATE.NAME AS STATE_NAME FROM TFOOD
                                    INNER JOIN TCATEGORY ON TCATEGORY.ID = TFOOD.CATEGORY_ID
                                    INNER JOIN TSTATE ON TSTATE.ID = TFOOD.STATE_ID 
                                    WHERE TFOOD.ID=@ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        food.ID = (int)reader["ID"];
                        food.TITLE = (string)reader["TITLE"];
                        food.DESCRIPTION = (string)reader["DESCRIPTION"];
                        food.PRICE = (decimal)reader["PRICE"];
                        food.IMAGE = (string)reader["IMAGE"];
                        food.CATEGORY_ID = (int)reader["CATEGORY_ID"];
                        food.CATEGORY_NAME = (string)reader["CATEGORY_NAME"];
                        food.STATE_ID = (int)reader["STATE_ID"];
                        food.STATE_NAME = (string)reader["STATE_NAME"];
                    }
                    con.Close();
                }
            }

            return food;
        }

        internal void Insert(FOOD food)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"INSERT INTO TFOOD (TITLE, DESCRIPTION, PRICE, IMAGE, CATEGORY_ID, STATE_ID) VALUES 
                                                       (@TITLE, @DESCRIPTION, @PRICE, @IMAGE, @CATEGORY_ID, @STATE_ID)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@TITLE", food.TITLE);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", food.DESCRIPTION);
                    cmd.Parameters.AddWithValue("@PRICE", food.PRICE);
                    cmd.Parameters.AddWithValue("@IMAGE", food.IMAGE);
                    cmd.Parameters.AddWithValue("@CATEGORY_ID", food.CATEGORY_ID);
                    cmd.Parameters.AddWithValue("@STATE_ID", food.STATE_ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Update(FOOD food)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"UPDATE TFOOD SET TITLE = @TITLE, DESCRIPTION = @DESCRIPTION, PRICE=@PRICE, IMAGE=@IMAGE, CATEGORY_ID=@CATEGORY_ID, STATE_ID=@STATE_ID WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@TITLE", food.TITLE);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", food.DESCRIPTION);
                    cmd.Parameters.AddWithValue("@PRICE", food.PRICE);
                    cmd.Parameters.AddWithValue("@IMAGE", food.IMAGE);
                    cmd.Parameters.AddWithValue("@CATEGORY_ID", food.CATEGORY_ID);
                    cmd.Parameters.AddWithValue("@STATE_ID", food.STATE_ID);
                    cmd.Parameters.AddWithValue("@ID", food.ID);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        internal void Delete(int id)
        {
            using (con = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"DELETE FROM TFOOD WHERE ID = @ID";

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