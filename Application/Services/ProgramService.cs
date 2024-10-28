using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProgramService> _logger;

        public ProgramService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProgramService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProgramDTO> GetProgramByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching program with ID: {id}");
            var program = await _unitOfWork.Programs.GetByIdAsync(id);

            if (program == null)
            {
                _logger.LogWarning($"Program with ID {id} not found.");
                throw new KeyNotFoundException($"Program with id {id} not found.");
            }

            _logger.LogInformation($"Program with ID {id} retrieved successfully.");
            return _mapper.Map<ProgramDTO>(program);
        }

        public async Task<IEnumerable<ProgramDTO>> GetAllProgramsAsync()
        {
            _logger.LogInformation("Fetching all programs.");
            var programs = await _unitOfWork.Programs.GetAllAsync();
            return _mapper.Map<IEnumerable<ProgramDTO>>(programs);
        }

        public async Task UpsertProgramAsync(ProgramDTO programDto)
        {
            _logger.LogInformation("Upserting a program.");

            var existingProgram = await _unitOfWork.Programs.GetByIdAsync(programDto.Id);

            if (existingProgram == null)
            {
                var program = _mapper.Map<Program>(programDto);
                await _unitOfWork.Programs.AddAsync(program);
                _logger.LogInformation("Program created successfully.");
            }
            else
            {
                _mapper.Map(programDto, existingProgram);
                _unitOfWork.Programs.Update(existingProgram);
                _logger.LogInformation("Program updated successfully.");
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProgramAsync(int id)
        {
            _logger.LogInformation($"Deleting program with ID: {id}");

            var program = await _unitOfWork.Programs.GetByIdAsync(id);
            if (program == null)
            {
                _logger.LogWarning($"Program with ID {id} not found.");
                throw new KeyNotFoundException($"Program with id {id} not found.");
            }

            _unitOfWork.Programs.Delete(program);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Program with ID {id} deleted successfully.");
        }
    }
}
