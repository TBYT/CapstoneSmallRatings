
using SmallRatings.Services;
using SmallRatings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SmallRatings.Services
{
    public class UserDAO : IUserDataService
    {
        //the string we make a database connection with 
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CST451;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int Delete(UserInfo user)
        {
            //delete a product by its id although we pass in the model... -1 if query does not succeed.
            int newIdNumber = -1;

            string sqlStatement = "DELETE From dbo.product WHERE productId = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", user.UserID);
                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newIdNumber;
        }
        
        public List<UserInfo> GetAllUsers()
        {
            List<UserInfo> foundProducts = new List<UserInfo>();
            string sqlStatement = "SELECT * FROM dbo.product";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement,connection);
                try
                {
                    /*connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
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

        public UserInfo GetUserByID(int id)
        {
            UserInfo foundUser = null;
            string sqlStatement = "SELECT * FROM [dbo].[Users] WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    /*while (reader.Read())
                    { //changed after submission of mile 2
                        foundProduct = new UserInfo { Id = (int)reader[0], Name = (string)reader[1], Price = (decimal)reader[2], Stock = (int)reader[3], Description = (string)reader[4] };
                    }*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundUser;
        }

        public bool LoginUser(LoginInfo user)
        {
            string sqlStatement = "SELECT * FROM [dbo].[Users] WHERE (USERNAME = @Username AND PASSWORD = @Password)";
            bool success = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return success;
        }

        public bool Insert(UserInfo user)
        {
            string sqlStatement = @"INSERT INTO [dbo].[Users]
           ([FIRSTNAME],[LASTNAME],[USERNAME],[PASSWORD],[EMAIL],[NUMBER],[AVATAR],[SUSPENDED]) VALUES (@FirstName,@LastName,@Username,@Password,@Email,NULL,NULL,NULL); SELECT SCOPE_IDENTITY()";
            bool success = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Email", user.Email);
                try
                {
                    connection.Open();
                    if (Convert.ToInt32(command.ExecuteScalar()) != 0)
                        success = true;

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
