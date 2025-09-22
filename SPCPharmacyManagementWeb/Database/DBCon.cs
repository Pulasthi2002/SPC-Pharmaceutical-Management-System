using System;
using MySql.Data.MySqlClient;

namespace MySqlConnectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connection string: adjust user and password as needed
            string connectionString = "server=127.0.0.1;uid=root;pwd=;database=spc_pharmacy_db;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connection successful!");

                    // Example: List all tables in the database
                    using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES;", conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Tables in database:");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString());
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
