using GestaContinua.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Domain.Repositories
{
    public interface IProgressRecordRepository
    {
        Task<ProgressRecord?> GetByIdAsync(Guid id);
        Task<ProgressRecord> CreateAsync(ProgressRecord progressRecord);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ProgressRecord>> GetByTaskIdAsync(Guid taskId);
    }
}