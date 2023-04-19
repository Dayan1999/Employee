using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EmpAPI.database_Access_Layer
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }

    }

    public class db
    {
        private readonly string _connectionString;

        public db(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User AddUser(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO Users (Name) VALUES (@Name);" +
                            "SELECT CAST(scope_identity() AS int)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                connection.Open();
                int id = (int)command.ExecuteScalar();
                return new User { Id = id, Name = name };
            }
        }

        public User GetRecordById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id, Name FROM Users WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
                return null;
            }
        }

        public void UpdateUser(int id, string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Users SET Name = @Name WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM Users WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

}

