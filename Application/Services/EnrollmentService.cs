using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq;

namespace Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task EnrollStudentInSubjects(int studentId, List<int> subjectIds)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new ArgumentException("El estudiante no existe.");

            if (subjectIds.Count > 3)
                throw new ArgumentException("Solo puedes seleccionar hasta 3 materias.");

            var subjects = await _unitOfWork.Subjects.GetSubjectsByIdsAsync(subjectIds);
            if (subjects.Any(s => s.ProgramId != student.ProgramId))
                throw new ArgumentException("Solo puedes seleccionar materias de tu programa.");

            var distinctTeacherIds = subjects.Select(s => s.TeacherId).Distinct().Count();
            if (distinctTeacherIds < subjectIds.Count)
                throw new ArgumentException("No puedes seleccionar materias con el mismo profesor.");

            foreach (var subjectId in subjectIds)
            {
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    SubjectId = subjectId
                };
                await _unitOfWork.Enrollments.AddAsync(enrollment);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClassWithClassmatesDTO>> GetClassesWithClassmatesAsync(int studentId)
        {
            var enrollments = await _unitOfWork.Enrollments.GetEnrollmentsWithClassmatesByStudentIdAsync(studentId);

            return enrollments.Select(e => new ClassWithClassmatesDTO
            {
                SubjectName = e.Subject.Name,
                Classmates = e.Subject.Enrollments
                    .Where(enrollment => enrollment.StudentId != studentId)
                    .Select(enrollment => enrollment.Student.FullName)
                    .ToList()
            });
        }

        public async Task<int> GetTotalEnrolledCreditsAsync(int studentId)
        {
            var enrollments = await _unitOfWork.Enrollments.GetEnrollmentsByStudentIdAsync(studentId);
            return enrollments.Sum(e => e.Subject.Credits);
        }


    }
}
