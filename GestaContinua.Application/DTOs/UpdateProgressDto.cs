using System;

namespace GestaContinua.Application.DTOs
{
    public class UpdateProgressDto
    {
        public Guid TaskId { get; set; }
        public object Value { get; set; } = new object();
    }
}