
using CybersecurityAwarenessBot.GUI.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot.GUI.Services
{
    public class TaskService
    {
        private readonly DatabaseService databaseService;

        public TaskService()
        {
            databaseService = new DatabaseService();
        }

        // Add a new task to MySQL.
        public async Task<int> AddTaskAsync(
            string title,
            string description,
            DateTime? reminderAt)
        {
            using var connection = databaseService.CreateConnection();
            await connection.OpenAsync();

            string sql = @"
                INSERT INTO CyberTasks
                (Title, Description, ReminderAt)
                VALUES
                (@Title, @Description, @ReminderAt);";

            using var command = new MySqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Description", description ?? "");

            object reminderValue = reminderAt.HasValue
                ? reminderAt.Value
                : DBNull.Value;

            command.Parameters.AddWithValue("@ReminderAt", reminderValue);

            await command.ExecuteNonQueryAsync();

            using var idCommand = new MySqlCommand(
                "SELECT LAST_INSERT_ID();", connection);

            return Convert.ToInt32(await idCommand.ExecuteScalarAsync());
        }

        // Get all saved tasks from MySQL.
        public async Task<List<CyberTask>> GetAllTasksAsync()
        {
            var tasks = new List<CyberTask>();

            using var connection = databaseService.CreateConnection();
            await connection.OpenAsync();

            string sql = @"
                SELECT TaskID, Title, Description, ReminderAt,
                       IsCompleted, CreatedAt, CompletedAt
                FROM CyberTasks
                ORDER BY CreatedAt DESC;";

            using var command = new MySqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var task = new CyberTask
                {
                    TaskID = reader.GetInt32("TaskID"),
                    Title = reader.GetString("Title"),
                    Description = reader.IsDBNull(
                        reader.GetOrdinal("Description"))
                        ? ""
                        : reader.GetString("Description"),

                    ReminderAt = reader.IsDBNull(
                        reader.GetOrdinal("ReminderAt"))
                        ? null
                        : reader.GetDateTime("ReminderAt"),

                    IsCompleted = reader.GetBoolean("IsCompleted"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),

                    CompletedAt = reader.IsDBNull(
                        reader.GetOrdinal("CompletedAt"))
                        ? null
                        : reader.GetDateTime("CompletedAt")
                };

                tasks.Add(task);
            }

            return tasks;
        }

        // Mark a task as completed.
        public async Task MarkTaskCompletedAsync(int taskId)
        {
            using var connection = databaseService.CreateConnection();
            await connection.OpenAsync();

            string sql = @"
                UPDATE CyberTasks
                SET IsCompleted = TRUE,
                    CompletedAt = NOW()
                WHERE TaskID = @TaskID;";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@TaskID", taskId);

            await command.ExecuteNonQueryAsync();
        }

        // Delete a task.
        public async Task DeleteTaskAsync(int taskId)
        {
            using var connection = databaseService.CreateConnection();
            await connection.OpenAsync();

            string sql = @"
                DELETE FROM CyberTasks
                WHERE TaskID = @TaskID;";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@TaskID", taskId);

            await command.ExecuteNonQueryAsync();
        }
    }
}
