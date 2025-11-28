using GestaContinua.Application.DTOs;
using GestaContinua.Application.UseCases;
using GestaContinua.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestaContinua.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly CreateTaskUseCase _createTaskUseCase;
        private readonly UpdateTaskStatusUseCase _updateTaskStatusUseCase;
        private readonly ITaskRepository _taskRepository;

        public TasksController(
            CreateTaskUseCase createTaskUseCase,
            UpdateTaskStatusUseCase updateTaskStatusUseCase,
            ITaskRepository taskRepository)
        {
            _createTaskUseCase = createTaskUseCase;
            _updateTaskStatusUseCase = updateTaskStatusUseCase;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var taskId = await _createTaskUseCase.ExecuteAsync(
                createTaskDto.UserId,
                createTaskDto.CategoryId,
                createTaskDto.Name,
                createTaskDto.Goal,
                createTaskDto.InputFormat,
                createTaskDto.Schedule,
                createTaskDto.Weight);

            return Ok(taskId);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetTasks([FromQuery] Guid userId)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<bool>> UpdateTaskStatus([FromRoute] Guid id, [FromBody] UpdateTaskStatusDto updateDto)
        {
            if (id != updateDto.TaskId)
            {
                return BadRequest("Task ID mismatch");
            }

            var result = await _updateTaskStatusUseCase.ExecuteAsync(updateDto.TaskId, updateDto.NewStatus);
            return Ok(result);
        }
    }
}