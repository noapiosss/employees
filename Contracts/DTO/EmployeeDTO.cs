using System;
using System.Text;

namespace Contracts.DTO
{
    public struct EmployeeDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly EmploymentDate { get; set; }
        public decimal Salary { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Id: {Id}");
            sb.AppendLine($"Name: {FirstName} {LastName} {Patronymic}");
            sb.AppendLine($"Department: {DepartmentName}");
            sb.AppendLine($"Position: {PositionName}");
            sb.AppendLine($"Salary: {Salary}");
            sb.AppendLine($"Phone: {Phone}");
            sb.AppendLine($"Address: {Address}");
            sb.AppendLine($"Birth date: {BirthDate:yyyy-MM-dd}");
            sb.AppendLine($"Employment date: {EmploymentDate:yyyy-MM-dd}");

            return sb.ToString();
        }
    }
}