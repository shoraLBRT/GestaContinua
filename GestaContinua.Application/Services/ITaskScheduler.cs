using GestaContinua.Domain.Entities;
using System;

namespace GestaContinua.Application.Services
{
    public interface ITaskScheduler
    {
        DateTime CalculateNextReminder(Domain.Entities.Task task, DateTime lastReminder);
        void RescheduleTask(Domain.Entities.Task task);
    }
}