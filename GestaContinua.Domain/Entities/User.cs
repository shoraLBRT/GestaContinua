using System;

namespace GestaContinua.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public long TelegramId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActiveAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; } = "Active";
    }
}