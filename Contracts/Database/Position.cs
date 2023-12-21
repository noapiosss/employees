using System.Collections.Generic;

namespace Contracts.Database
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DepartmentPosition> Departments { get; set; }
    }
}