using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetSubjectsByProgramAsync(int id);
        Task<IEnumerable<Subject>> GetSubjectsByIdsAsync(IEnumerable<int> ids);
    }
}