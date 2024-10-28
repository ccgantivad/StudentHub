using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentHubDbContext _context;

        private IStudentRepository? _students;
        private ISubjectRepository? _subjects;
        private IRepository<Teacher>? _teachers;
        private IEnrollmentRepository? _enrollments;
        private IRepository<Program>? _programs;

        public UnitOfWork(StudentHubDbContext context)
        {
            _context = context;
        }

        public IStudentRepository Students => _students ??= new StudentRepository(_context);
        public ISubjectRepository Subjects => _subjects ??= new SubjectRepository(_context);
        public IRepository<Teacher> Teachers => _teachers ??= new Repository<Teacher>(_context);
        public IEnrollmentRepository Enrollments => _enrollments ??= new EnrollmentRepository(_context);
        public IRepository<Program> Programs=> _programs??= new Repository<Program>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
