using GestaContinua.Domain.Entities;
using System;

namespace GestaContinua.Application.Services
{
    public interface ITaskScheduler
    {
        DateTime CalculateNextReminder(Task task, DateTime lastReminder);
        void RescheduleTask(Task task);
    }
}