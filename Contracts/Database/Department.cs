using System.Collections.Generic;

namespace Contracts.Database
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DepartmentPosition> Positions { get; set; }
    }
}