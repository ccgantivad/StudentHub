using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramById(int id)
        {
            var program = await _programService.GetProgramByIdAsync(id);
            return Ok(program);
        }
        [HttpPost]
        [HttpPut]
        public async Task<IActionResult> UpsertProgram([FromBody] ProgramDTO programDto)
        {
            await _programService.UpsertProgramAsync(programDto);
            return Ok(new { message = "Program upserted successfully." });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPrograms()
        {
            var programs = await _programService.GetAllProgramsAsync();
            return Ok(programs);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            await _programService.DeleteProgramAsync(id);
            return NoContent();
        }
    }
}
