
using System;

namespace CybersecurityAwarenessBot.GUI.Models
{
    public class CyberTask
    {
        public int TaskID { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime? ReminderAt { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
