using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EnrollmentRequestDTO
    {
        public required int StudentId { get; set; }
        public required List<int> SubjectIds { get; set; }
    }
}
