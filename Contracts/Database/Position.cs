using System.Collections.Generic;

namespace Contracts.Database
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}