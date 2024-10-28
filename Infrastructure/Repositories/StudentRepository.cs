using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(StudentHubDbContext context) : base(context)
        {
        }

        public async Task<Student> GetStudentByUsernameAsync(string username)
        {
            return await _context.Set<Student>()
                .FirstOrDefaultAsync(s => s.Username == username);
        }

        public async Task<IEnumerable<Student>> GetAllWithDetailsAsync()
        {
            return await _context.Set<Student>()
                .Include(s => s.Program)              
                .Include(s => s.Enrollments)          
                .ThenInclude(e => e.Subject)          
                .ThenInclude(subject => subject.Teacher) 
                .ToListAsync();
        }
    }
}
