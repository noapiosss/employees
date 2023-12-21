using System.Collections.Generic;

namespace Contracts.Database
{
    public class DepartmentPosition
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}