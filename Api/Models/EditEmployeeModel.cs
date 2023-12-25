using System.Collections.Generic;
using Contracts.DTO;

namespace Api.Models
{
    public class EditEmployeeModel
    {
        public EmployeeDTO Employee { get; init; }
        public List<DepartmentDTO> AllDepartments { get; init; }
        public List<PositionDTO> AllPositions { get; init; }
    }
}