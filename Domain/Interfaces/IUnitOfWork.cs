using Domain.Entities;

namespace Domain.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }

        ISubjectRepository Subjects { get; }
        IRepository<Teacher> Teachers { get; }
        IEnrollmentRepository  Enrollments { get; }
        IRepository<Program> Programs{ get; }

    Task<int> SaveChangesAsync();
}
  

}
