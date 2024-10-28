using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(StudentHubDbContext context) : base(context)
        {
        }
    
        public async Task<IEnumerable< Subject>> GetSubjectsByProgramAsync(int id)
        {
            return await _context.Set<Subject>()
                         .Where(s => s.ProgramId == id)
                         .Include(s => s.Teacher)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Set<Subject>()
                .Where(s => ids.Contains(s.Id))
                .Include(s => s.Teacher) 
                .ToListAsync();
        }
    }
}
