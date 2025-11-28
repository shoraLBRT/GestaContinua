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
    public class ProgressController : ControllerBase
    {
        private readonly ProcessUserResponseUseCase _processUserResponseUseCase;
        private readonly IProgressRecordRepository _progressRecordRepository;

        public ProgressController(
            ProcessUserResponseUseCase processUserResponseUseCase,
            IProgressRecordRepository progressRecordRepository)
        {
            _processUserResponseUseCase = processUserResponseUseCase;
            _progressRecordRepository = progressRecordRepository;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> RecordProgress([FromBody] UpdateProgressDto progressDto)
        {
            var result = await _processUserResponseUseCase.ExecuteAsync(progressDto.TaskId, progressDto.Value);
            return Ok(result);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<object>> GetProgress([FromRoute] Guid taskId)
        {
            var progressRecords = await _progressRecordRepository.GetByTaskIdAsync(taskId);
            return Ok(progressRecords);
        }
    }
}