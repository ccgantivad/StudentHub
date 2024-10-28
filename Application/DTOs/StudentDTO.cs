using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int ProgramId { get; set; }
    }

}
