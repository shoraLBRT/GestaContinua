using System;

namespace GestaContinua.Application.DTOs
{
    public class UpdateTaskStatusDto
    {
        public Guid TaskId { get; set; }
        public string NewStatus { get; set; } = string.Empty;
    }
}