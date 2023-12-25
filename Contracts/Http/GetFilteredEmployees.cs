using System;

namespace Contracts.Http
{
    public class GetFilteredEmployeesRequest
    {
        public int DepartmentId { get; init; }
        public int PositionId { get; init; }
        public BoundFilterValues BoundFilterValues { get; init; }        
        public string SearchRequest { get; init; }
    }   

    public class BoundFilterValues
    {
        public DateTime MinEmploymentDate { get; init; }
        public DateTime MaxEmploymentDate { get; init; }
        public int MinAge { get; init; }
        public int MaxAge { get; init; }
        public decimal MinSalary { get; init; }
        public decimal MaxSalary { get; init; }
    }

    public struct GetFilteredEmployeesResponse
    {
        public int Id { get; set; }
    }
}