using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<SubjectsController> _logger;

        public SubjectsController(ISubjectService subjectService, ILogger<SubjectsController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("program/{programId}")]
        public async Task<IActionResult> GetSubjectsByProgram(int programId)
        {
            try
            {
                var subjects = await _subjectService.GetSubjectsByProgramAsync(programId);
                if (subjects == null || !subjects.Any())
                {
                    _logger.LogInformation("No subjects found for Program ID: {ProgramId}", programId);
                    return NotFound(ApiResponseFactory.Error<object>("No subjects found for the specified program."));
                }

                _logger.LogInformation("Subjects retrieved successfully for Program ID: {ProgramId}", programId);
                return Ok(ApiResponseFactory.Success(subjects, "Subjects retrieved successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving subjects for Program ID: {ProgramId}", programId);
                return StatusCode(500, ApiResponseFactory.Error<object>("An unexpected error occurred."));
            }
        }
    }
}
