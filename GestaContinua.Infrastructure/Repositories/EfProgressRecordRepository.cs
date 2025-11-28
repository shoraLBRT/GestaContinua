using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using GestaContinua.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Infrastructure.Repositories
{
    public class EfProgressRecordRepository : IProgressRecordRepository
    {
        private readonly GestaContinuaDbContext _context;

        public EfProgressRecordRepository(GestaContinuaDbContext context)
        {
            _context = context;
        }

        public async Task<ProgressRecord?> GetByIdAsync(Guid id)
        {
            return await _context.ProgressRecords.FindAsync(id);
        }

        public async Task<ProgressRecord> CreateAsync(ProgressRecord progressRecord)
        {
            progressRecord.Id = Guid.NewGuid();
            progressRecord.RecordedAt = DateTime.UtcNow;
            
            _context.ProgressRecords.Add(progressRecord);
            await _context.SaveChangesAsync();
            return progressRecord;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var record = await _context.ProgressRecords.FindAsync(id);
            if (record == null) return false;

            _context.ProgressRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProgressRecord>> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.ProgressRecords
                .Where(p => p.TaskId == taskId)
                .ToListAsync();
        }
    }
}