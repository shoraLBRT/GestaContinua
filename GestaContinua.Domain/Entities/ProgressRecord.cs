using System;

namespace GestaContinua.Domain.Entities
{
    public class ProgressRecord
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public object Value { get; set; } = new object();
        public DateTime RecordedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}