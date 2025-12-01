using System;

namespace GestaContinua.Application.DTOs
{
    public class ProgressRecordDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public object? Value { get; set; }
        public DateTime RecordedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}