using System.Collections.Generic;
using System.Text;

namespace Contracts.DTO
{
    public struct DepartmentInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }
        public decimal AvgSalary { get; set; }
        public float AvgAge { get; set; }
        public int VacanciesCount { get; set; }
        public List<EmployeeDTO> Employees { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"{Name} department");
            sb.AppendLine($"Employees count: {Employees.Count}");
            sb.AppendLine($"Average age: {AvgAge}");
            sb.AppendLine($"Vacancies count: {VacanciesCount}");
            sb.AppendLine($"Total salary: {TotalSalary}");
            sb.AppendLine($"Mininal salary: {MinSalary}");
            sb.AppendLine($"Maximal salary: {MaxSalary}");
            sb.AppendLine($"Average salary: {AvgSalary}");
            sb.AppendLine($"------------------------------------------");
            sb.AppendLine($"Employees:");
            sb.AppendLine($"------------------------------------------");
            foreach(EmployeeDTO employeeDTO in Employees)
            {
                sb.AppendLine(employeeDTO.ToString());
                sb.AppendLine($"------------------------------------------");
            }

            return sb.ToString();
        }
    }
}