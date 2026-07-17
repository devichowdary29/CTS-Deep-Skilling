using System;

namespace EmployeeCrudApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public bool Permanent { get; set; }
    }
}
