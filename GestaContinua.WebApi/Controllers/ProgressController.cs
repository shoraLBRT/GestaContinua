using GestaContinua.Application.DTOs;
using GestaContinua.Application.UseCases;
using GestaContinua.Domain.Repositories;
using GestaContinua.Application.Services;
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
        private readonly IProgressRecordMappingService _mappingService;

        public ProgressController(
            ProcessUserResponseUseCase processUserResponseUseCase,
            IProgressRecordRepository progressRecordRepository,
            IProgressRecordMappingService mappingService)
        {
            _processUserResponseUseCase = processUserResponseUseCase;
            _progressRecordRepository = progressRecordRepository;
            _mappingService = mappingService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> RecordProgress([FromBody] UpdateProgressDto progressDto)
        {
            var result = await _processUserResponseUseCase.ExecuteAsync(progressDto.TaskId, progressDto.Value);
            return Ok(result);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<IEnumerable<ProgressRecordDto>>> GetProgress([FromRoute] Guid taskId)
        {
            var progressRecords = await _progressRecordRepository.GetByTaskIdAsync(taskId);
            var dtos = _mappingService.ToDtoList(progressRecords);
            return Ok(dtos);
        }
    }
}