
using MySqlConnector;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CybersecurityAwarenessBot.GUI.Services
{
    public class DatabaseService
    {
        public MySqlConnection CreateConnection()
        {
            string password =
                Environment.GetEnvironmentVariable("CYBERGUARD_DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "CYBERGUARD_DB_PASSWORD was not found. " +
                    "Please check your Windows environment variables.");
            }

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3306,
                Database = "cybersecurity_chatbot",
                UserID = "root",
                Password = password
            };

            return new MySqlConnection(builder.ConnectionString);
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = CreateConnection();

                await connection.OpenAsync();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Database Connection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return false;
            }
        }
    }
}
