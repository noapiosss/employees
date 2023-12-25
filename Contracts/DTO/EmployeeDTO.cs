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

            sb.Append($"Id: {Id}|");
            sb.Append($"Name: {FirstName} {LastName} {Patronymic}|");
            sb.Append($"Department: {DepartmentName}|");
            sb.Append($"Position: {PositionName}|");
            sb.Append($"Salary: {Salary}|");
            sb.Append($"Phone: {Phone}|");
            sb.Append($"Address: {Address}|");
            sb.Append($"Birth date: {BirthDate:yyyy-MM-dd}|");
            sb.Append($"Employment date: {EmploymentDate:yyyy-MM-dd}|");

            return sb.ToString();
        }
    }
}