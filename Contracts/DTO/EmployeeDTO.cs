using System;

namespace Contracts.Database
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
    }
}