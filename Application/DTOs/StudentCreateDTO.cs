using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StudentCreateDTO : StudentDTO
    {
        public required string PasswordHash {  get; set; }
    }

}
