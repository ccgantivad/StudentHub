using Application.DTOs;

namespace Application.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetSubjectsByProgramAsync(int programId);
    }
}
