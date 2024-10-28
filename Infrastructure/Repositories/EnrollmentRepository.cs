using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(StudentHubDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithClassmatesByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Include(e => e.Subject)
                .ThenInclude(s => s.Enrollments) 
                .ThenInclude(e => e.Student)
                .Where(e => e.StudentId == studentId) 
                .ToListAsync();
        }
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Subject) 
                .ToListAsync();
        }
    }
}
