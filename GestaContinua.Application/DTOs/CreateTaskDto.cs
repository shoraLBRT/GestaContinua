using System;

namespace GestaContinua.Application.DTOs
{
    public class CreateTaskDto
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Goal { get; set; }
        public string InputFormat { get; set; } = "Boolean";
        public string Schedule { get; set; } = "Daily";
        public int Weight { get; set; } = 1;
    }
}