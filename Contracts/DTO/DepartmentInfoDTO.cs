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

            sb.Append($"{Name} department|");
            sb.Append($"Employees count: {Employees.Count}|");
            sb.Append($"Average age: {AvgAge}|");
            sb.Append($"Vacancies count: {VacanciesCount}|");
            sb.Append($"Total salary: {TotalSalary}|");
            sb.Append($"Mininal salary: {MinSalary}|");
            sb.Append($"Maximal salary: {MaxSalary}|");
            sb.Append($"Average salary: {AvgSalary}|");
            sb.Append($"------------------------------------------|");
            foreach(EmployeeDTO employeeDTO in Employees)
            {
                sb.Append(employeeDTO.ToString());
                sb.Append($"------------------------------------------|");
            }

            return sb.ToString();
        }
    }
}