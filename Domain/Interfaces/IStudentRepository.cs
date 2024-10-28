using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetStudentByUsernameAsync(string username);
        Task<IEnumerable<Student>> GetAllWithDetailsAsync(); // Método para obtener todos los estudiantes con detalles
    }
}