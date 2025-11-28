using System;

namespace GestaContinua.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Goal { get; set; }
        public double Progress { get; set; }
        public int Weight { get; set; } = 1;
        public string Schedule { get; set; } = "Daily";
        public string Status { get; set; } = "Active";
        public DateTime? NextReminderAt { get; set; }
        public string InputFormat { get; set; } = "Boolean";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}