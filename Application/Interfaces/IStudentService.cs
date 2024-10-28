using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();        
        Task DeleteStudentAsync(int id);
        Task<StudentDTO> GetStudentByUsernameAsync(string username);
        Task CreateStudentAsync(StudentCreateDTO studentDto);
        Task<IEnumerable<StudentRecordDTO>> GetAllStudentsWithRecordsAsync();



    }

}
