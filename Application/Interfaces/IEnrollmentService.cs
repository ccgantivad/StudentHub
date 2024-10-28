using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task EnrollStudentInSubjects(int studentId, List<int> subjectIds);
        Task<IEnumerable<ClassWithClassmatesDTO>> GetClassesWithClassmatesAsync(int studentId);
        Task<int> GetTotalEnrolledCreditsAsync(int studentId);
    }
}

