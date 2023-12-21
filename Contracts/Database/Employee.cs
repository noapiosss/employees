using System;

namespace Contracts.Database
{
    public class Employee
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public DepartmentPosition DepartmentPosition { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public decimal Salary { get; set; }
    }
}