using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;


        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SubjectDTO>> GetSubjectsByProgramAsync(int programId)
        {
            _logger.LogInformation($"Fetching subjects by program: {programId}");

            var subjects = await _unitOfWork.Subjects.GetSubjectsByProgramAsync(programId);
            return _mapper.Map<IEnumerable<SubjectDTO>>(subjects);
        }
    }
}
