using System.Collections.Generic;
using Contracts.DTO;
using Contracts.Http;

namespace Api.Models
{
    public class EmployeesViewModel
    {
        public List<EmployeeDTO> Employees { get; init; }
        public BoundValues BoundValues { get; init; }
        public GetFilteredEmployeesRequest Filters { get; init; }
        public int Page { get; init; }
        public int PagesCount { get; init; }
        public int PerPage { get; init; }
    }

    public class BoundValues
    {
        public List<DepartmentDTO> Departmens { get; init; }
        public List<PositionDTO> Positions { get; init; }
        public BoundFilterValues BoundFilterValues { get; init ;}
    }
}