using System;

namespace Contracts.Http
{
    public struct CreateEmployeeRequest
    {
        public int DepartmentPositionId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Patronymic { get; init; }
        public string Address { get; init; }
        public string Phone { get; init; }
        public DateTime BirthDate { get; init; }
        public DateTime EmploymentDate { get; init; }
        public decimal Salary { get; init; }
    }

    public struct CreateEmployeeResponse
    {
        public int Id { get; init; }
    }
}