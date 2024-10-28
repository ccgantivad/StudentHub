using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger<EnrollmentController> _logger;

        public EnrollmentController(IEnrollmentService enrollmentService, ILogger<EnrollmentController> logger)
        {
            _enrollmentService = enrollmentService;
            _logger = logger;
        }

        [HttpPost("enroll")]
        [AllowAnonymous]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollmentRequestDTO request)
        {
            if (request == null || request.SubjectIds == null || !request.SubjectIds.Any())
            {
                _logger.LogError("Invalid enrollment request format: {Request}", request);
                return BadRequest(ApiResponseFactory.Error<object>("The enrollment request format is incorrect."));
            }

            try
            {
                await _enrollmentService.EnrollStudentInSubjects(request.StudentId, request.SubjectIds);
                return Ok(ApiResponseFactory.Success<object>(null, "Student enrolled successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Student or subjects not found for enrollment.");
                return NotFound(ApiResponseFactory.Error<object>("Student or subjects not found."));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation when attempting to enroll student.");
                return BadRequest(ApiResponseFactory.Error<object>("Enrollment is not valid: " + ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while enrolling the student.");
                return StatusCode(500, ApiResponseFactory.Error<object>("An unexpected error occurred."));
            }
        }

        [HttpGet("student/{studentId}/classmates")]
        public async Task<IActionResult> GetClassmatesByStudent(int studentId)
        {
            try
            {
                var classesWithClassmates = await _enrollmentService.GetClassesWithClassmatesAsync(studentId);
                return Ok(ApiResponseFactory.Success(classesWithClassmates, "Classes and classmates retrieved successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Student not found with ID: {StudentId}", studentId);
                return NotFound(ApiResponseFactory.Error<object>("Student not found."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving the student's classes and classmates.");
                return StatusCode(500, ApiResponseFactory.Error<object>("An unexpected error occurred."));
            }
        }

        [HttpGet("credits/{studentId}")]
        public async Task<IActionResult> GetTotalEnrolledCredits(int studentId)
        {
            try
            {
                int totalCredits = await _enrollmentService.GetTotalEnrolledCreditsAsync(studentId);
                return Ok(ApiResponseFactory.Success(totalCredits, "Total enrolled credits retrieved successfully."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Student not found with ID: {StudentId}", studentId);
                return NotFound(ApiResponseFactory.Error<object>("Student not found."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving enrolled credits for student ID: {StudentId}", studentId);
                return StatusCode(500, ApiResponseFactory.Error<object>("An unexpected error occurred while retrieving credits."));
            }
        }
    }
}
