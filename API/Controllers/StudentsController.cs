using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(ApiResponseFactory.Success(students, "Estudiantes recuperados exitosamente."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al recuperar los estudiantes.");
                return StatusCode(500, ApiResponseFactory.Error<IEnumerable<StudentDTO>>("Ocurrió un error al recuperar los estudiantes."));
            }
        }        
        [HttpGet("all-students")]
        public async Task<IActionResult> GetAllStudentRecords()
        {
            try
            {
                var students = await _studentService.GetAllStudentsWithRecordsAsync();
                return Ok(ApiResponseFactory.Success(students, "Registros detallados de estudiantes recuperados exitosamente."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al recuperar los registros de estudiantes.");
                return StatusCode(500, ApiResponseFactory.Error<IEnumerable<StudentRecordDTO>>("Ocurrió un error al recuperar los registros de estudiantes."));
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                return Ok(ApiResponseFactory.Success(student, "Estudiante recuperado exitosamente."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"El estudiante con ID {id} no fue encontrado.");
                return NotFound(ApiResponseFactory.Error<StudentDTO>("Estudiante no encontrado."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al recuperar el estudiante.");
                return StatusCode(500, ApiResponseFactory.Error<StudentDTO>("Ocurrió un error al recuperar el estudiante."));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDTO studentCreateDto)
        {
            if (studentCreateDto == null)
            {
                _logger.LogWarning("Solicitud de creación de estudiante es nula.");
                return BadRequest(ApiResponseFactory.Error<object>("Datos de estudiante inválidos."));
            }

            try
            {
                await _studentService.CreateStudentAsync(studentCreateDto);
                return CreatedAtAction(nameof(GetStudentById), new { id = studentCreateDto.Id },
                    ApiResponseFactory.Success(studentCreateDto, "Estudiante creado exitosamente."));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "El nombre de usuario ya está en uso.");
                return Conflict(ApiResponseFactory.Error<object>("El nombre de usuario ya está en uso."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al crear el estudiante.");
                return StatusCode(500, ApiResponseFactory.Error<object>("Ocurrió un error al crear el estudiante."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok(ApiResponseFactory.Success<object?>(null, "Estudiante eliminado exitosamente."));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"El estudiante con ID {id} no fue encontrado.");
                return NotFound(ApiResponseFactory.Error<object>("Estudiante no encontrado."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error durante la eliminación.");
                return StatusCode(500, ApiResponseFactory.Error<object>("Ocurrió un error durante la eliminación."));
            }
        }
    }
}
