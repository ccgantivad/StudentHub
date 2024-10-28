using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Credits { get; set; }

        public int TeacherId { get; set; }

        public int ProgramId { get; set; }

        public string? TeacherName { get; set; }

    }
}
