using System.Collections.Generic;

namespace Contracts.Database
{
    public struct DepartmentInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }
        public decimal AvgSalary { get; set; }
        public List<EmployeeDTO> Eployees { get; set; }
    }
}