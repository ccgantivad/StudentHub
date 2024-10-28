using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching student with ID: {id}");
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
            {
                _logger.LogWarning($"Student with ID {id} not found.");
                throw new KeyNotFoundException($"Student with id {id} not found.");
            }

            _logger.LogInformation($"Student with ID {id} retrieved successfully.");
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            _logger.LogInformation("Fetching all students.");
            var students = await _unitOfWork.Students.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<IEnumerable<StudentRecordDTO>> GetAllStudentsWithRecordsAsync()
        {
            var students = await _unitOfWork.Students.GetAllWithDetailsAsync(); 
            return students.Select(student => new StudentRecordDTO
            {
                Id = student.Id,
                FullName = student.FullName,
                ProgramName = student.Program.Name,
                Subjects = student.Enrollments.Select(e => new SubjectDTO
                {
                    Id = e.Subject.Id,
                    Name = e.Subject.Name,
                    TeacherName = e.Subject.Teacher.Name,
                    Credits = e.Subject.Credits
                }).ToList()
            });
        } 


        public async Task DeleteStudentAsync(int id)
        {
            _logger.LogInformation($"Deleting student with ID: {id}");
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
            {
                _logger.LogWarning($"Student with ID {id} not found.");
                throw new KeyNotFoundException($"Student with id {id} not found");
            }

            _unitOfWork.Students.Delete(student);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Student with ID {id} deleted successfully.");
        }
        public async Task<StudentDTO> GetStudentByUsernameAsync(string username)
        {
            _logger.LogInformation($"Fetching student with username: {username}");
            var student = await _unitOfWork.Students.GetStudentByUsernameAsync(username);

            if (student == null)
            {
                _logger.LogWarning($"Student with username {username} not found.");
                throw new KeyNotFoundException($"Student with username {username} not found.");
            }

            _logger.LogInformation($"Student with username {username} retrieved successfully.");
            return _mapper.Map<StudentDTO>(student);
        }
        public async Task CreateStudentAsync(StudentCreateDTO studentDto)
        {
            _logger.LogInformation("Creating a new student.");

            var existingStudent = await _unitOfWork.Students.GetStudentByUsernameAsync(studentDto.Username);
            if (existingStudent != null)
            {
                throw new ArgumentException("Username is already taken.");
            }

            var student = _mapper.Map<Student>(studentDto);

            var (hash, key) = PasswordHelper.HashPassword(studentDto.PasswordHash);
            student.PasswordHash = hash;
            student.PasswordKey = key;

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Student created successfully.");
        }

    }
}
