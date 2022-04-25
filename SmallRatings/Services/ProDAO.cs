using SmallRatings.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;

namespace SmallRatings.Services
{
    //NOT OPTIMIZED YET, will do so.
    public class ProDAO : IProDataService
    {
        //the string we make a database connection with
        string connectionString = Environment.GetEnvironmentVariable("DAOString");

        public List<UserInfo> GetAllUsers()
        {
            List<UserInfo> foundProducts = new List<UserInfo>();
            string sqlStatement = "SELECT * FROM product";

            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement,connection);
                try
                {
                    /*connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) //changed after submission of mile 2
                    {
                        foundProducts.Add(new UserInfo { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Stock = (int)reader[3], Description = (string)reader[4]});
                    }*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return foundProducts;
        }

        public ProInfo ReturnPro(int id)
        {
            string sqlStatement = "SELECT * FROM professionals WHERE (ID = @ID)";
            ProInfo proInfo = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        proInfo = new ProInfo();
                        proInfo.UserID = Convert.ToInt32(reader["ID"]);
                        proInfo.ProEmail = reader["PROEMAIL"].ToString();
                        proInfo.ProName = reader["PRONAME"].ToString();
                        proInfo.Website = reader["PROWEBSITE"].ToString();
                        proInfo.Description = reader["PRODESCRIPTION"].ToString();
                        proInfo.Location = reader["LOCATION"].ToString();
                        if (!(reader["PROAVATAR"].GetType().Equals(typeof(DBNull))))
                        {
                            proInfo.ProAvatar = (byte[])(reader["PROAVATAR"]);
                        }
                        proInfo.AvatarFileType = reader["AVATARTYPE"].ToString();
                        if (!(reader["PROHEADER"].GetType().Equals(typeof(DBNull))))
                        {
                            proInfo.ProHeader = (byte[])(reader["PROHEADER"]);
                        }
                        proInfo.HeaderFileType = reader["HEADERTYPE"].ToString();
                    }

                    connection.Close();
                }
            }
            return proInfo;
        }

        public bool UpdateBiz(ProInfo obj)
        {
            bool success = false;

            string sqlStatement = "UPDATE professionals SET PRONAME = @ProName, PROEMAIL = @ProEmail, PROWEBSITE = @ProWebsite, PRODESCRIPTION = @Description, LOCATION = @Location,PROAVATAR = @Avatar, AVATARTYPE = @AvatarType, PROHEADER = @ProHeader, HEADERTYPE = @HeaderType WHERE ID = @ID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Id", obj.ProID);
                command.Parameters.AddWithValue("@Location", obj.Location);
                command.Parameters.AddWithValue("@ProName", obj.ProName);
                command.Parameters.AddWithValue("@ProEmail", obj.ProEmail);
                command.Parameters.AddWithValue("@Description", MySqlHelper.EscapeString(obj.Description));
                command.Parameters.AddWithValue("@ProWebsite", MySqlHelper.EscapeString(obj.Website));
                if (obj.ProAvatar == null)
                    command.Parameters.AddWithValue("@Avatar", SqlBinary.Null);
                else
                    command.Parameters.AddWithValue("@Avatar", obj.ProAvatar);
                if (obj.AvatarFileType == null)
                    command.Parameters.AddWithValue("@AvatarType", SqlString.Null);
                else
                    command.Parameters.AddWithValue("@AvatarType", obj.AvatarFileType);
                if (obj.ProHeader == null)
                    command.Parameters.AddWithValue("@ProHeader", SqlBinary.Null);
                else
                    command.Parameters.AddWithValue("@ProHeader", obj.ProHeader);
                if (obj.HeaderFileType == null)
                    command.Parameters.AddWithValue("@HeaderType", SqlString.Null);
                else
                    command.Parameters.AddWithValue("@HeaderType", obj.HeaderFileType);

                if (command.ExecuteNonQuery() != 0)
                {
                    success = true;
                }

                connection.Close();
            }
            return success;
        }

        public bool CheckDuplication(ProInfo proInfo)
        {
            string sqlStatement = "SELECT * FROM professionals WHERE (PRONAME = @ProName)";
            bool success = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    command.Parameters.AddWithValue("@ProName", proInfo.ProName);
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        success = true;
                    }

                    connection.Close();
                }
            }
            return success;
        }

        public int GetProID(int userId)
        {
            string sqlStatement = "SELECT ID FROM professionals WHERE (USERID = @userID)";
            int ReturnUserId = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReturnUserId = Convert.ToInt32(reader["ID"]);
                    }

                    connection.Close();
                }
            }
            return ReturnUserId;
        }

        public int NewBusiness(ProInfo proInfo)
        {
            string sqlStatement = "INSERT INTO `professionals` (PRONAME,PROEMAIL,PROWEBSITE,PRODESCRIPTION,PROAVATAR,PROHEADER,USERID,AVATARTYPE,HEADERTYPE,LOCATION) VALUES (@ProName,@ProEmail,@ProWebsite,@ProDescription,@ProAvatar,@ProHEader,@UserID,@AvatarType,@HeaderType,@Location); SELECT LAST_INSERT_ID();";
            int success = -1; //can return 0 if no rows are updated

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@UserID", proInfo.UserID);
                command.Parameters.AddWithValue("@ProName", proInfo.ProName);
                command.Parameters.AddWithValue("@ProEmail", proInfo.ProEmail);
                command.Parameters.AddWithValue("@ProWebsite", proInfo.Website);
                command.Parameters.AddWithValue("@ProDescription", proInfo.Description);
                command.Parameters.AddWithValue("@Location", proInfo.Location);
                command.Parameters.AddWithValue("@ProAvatar", SqlBinary.Null);
                command.Parameters.AddWithValue("@AvatarType", SqlString.Null);
                command.Parameters.AddWithValue("@ProHeader", SqlBinary.Null);
                command.Parameters.AddWithValue("@HeaderType", SqlString.Null);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        success = Convert.ToInt32(reader["LAST_INSERT_ID()"]);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return success;
        }
    }
}
