using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StudentRecordDTO
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string ProgramName { get; set; }
        public List<SubjectDTO>? Subjects { get; set; }
    }

}
