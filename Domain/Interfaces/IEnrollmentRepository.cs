using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEnrollmentRepository: IRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentsWithClassmatesByStudentIdAsync(int studentId);

        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
    }
}