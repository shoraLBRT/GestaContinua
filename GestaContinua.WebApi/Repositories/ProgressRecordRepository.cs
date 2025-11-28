using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaContinua.WebApi.Repositories
{
    public class ProgressRecordRepository : IProgressRecordRepository
    {
        private static readonly List<ProgressRecord> _progressRecords = new List<ProgressRecord>();

        public Task<ProgressRecord?> GetByIdAsync(Guid id)
        {
            var record = _progressRecords.Find(p => p.Id == id);
            return Task.FromResult(record);
        }

        public Task<ProgressRecord> CreateAsync(ProgressRecord progressRecord)
        {
            progressRecord.Id = Guid.NewGuid();
            _progressRecords.Add(progressRecord);
            return Task.FromResult(progressRecord);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var record = _progressRecords.Find(p => p.Id == id);
            if (record != null)
            {
                _progressRecords.Remove(record);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<ProgressRecord>> GetByTaskIdAsync(Guid taskId)
        {
            var records = _progressRecords.Where(p => p.TaskId == taskId).ToList();
            return Task.FromResult(records.AsEnumerable());
        }
    }
}