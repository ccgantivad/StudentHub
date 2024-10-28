using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProgramService
    {
      
            Task<ProgramDTO> GetProgramByIdAsync(int id);
            Task<IEnumerable<ProgramDTO>> GetAllProgramsAsync();
            Task UpsertProgramAsync(ProgramDTO programDto);
            Task DeleteProgramAsync(int id);
      
    }
}
