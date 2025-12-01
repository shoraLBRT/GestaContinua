using GestaContinua.Application.DTOs;
using GestaContinua.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GestaContinua.Application.Services
{
    public interface IProgressRecordMappingService
    {
        ProgressRecordDto ToDto(ProgressRecord record);
        IEnumerable<ProgressRecordDto> ToDtoList(IEnumerable<ProgressRecord> records);
        ProgressRecord FromDto(ProgressRecordDto dto);
    }

    public class ProgressRecordMappingService : IProgressRecordMappingService
    {
        public ProgressRecordDto ToDto(ProgressRecord record)
        {
            if (record == null) return null;

            object? value = null;
            if (!string.IsNullOrEmpty(record.ValueJson))
            {
                try
                {
                    value = JsonSerializer.Deserialize<object>(record.ValueJson);
                }
                catch (JsonException)
                {
                    // Если десериализация не удалась, возвращаем null или исходную JSON-строку
                    value = record.ValueJson;
                }
            }

            return new ProgressRecordDto
            {
                Id = record.Id,
                TaskId = record.TaskId,
                Value = value,
                RecordedAt = record.RecordedAt,
                IsCompleted = record.IsCompleted
            };
        }

        public IEnumerable<ProgressRecordDto> ToDtoList(IEnumerable<ProgressRecord> records)
        {
            var result = new List<ProgressRecordDto>();
            foreach (var record in records)
            {
                var dto = ToDto(record);
                if (dto != null)
                {
                    result.Add(dto);
                }
            }
            return result;
        }

        public ProgressRecord FromDto(ProgressRecordDto dto)
        {
            if (dto == null) return null;

            var jsonValue = dto.Value != null ? JsonSerializer.Serialize(dto.Value) : "{}";

            return new ProgressRecord
            {
                Id = Guid.Empty, // Will be set by EF
                TaskId = dto.TaskId,
                ValueJson = jsonValue,
                RecordedAt = dto.RecordedAt,
                IsCompleted = dto.IsCompleted
            };
        }
    }
}