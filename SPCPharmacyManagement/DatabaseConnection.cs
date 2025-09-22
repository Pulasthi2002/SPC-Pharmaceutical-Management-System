using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SPCPharmacyManagement.Services
{
    public class DatabaseConnection
    {
        private static string connectionString = "Server=localhost;Database=spc_pharmacy_db;Uid=root;Pwd=;CharSet=utf8;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database connection failed: {ex.Message}");
                return false;
            }
        }

        public static void SetConnectionString(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};CharSet=utf8;";
        }

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}
