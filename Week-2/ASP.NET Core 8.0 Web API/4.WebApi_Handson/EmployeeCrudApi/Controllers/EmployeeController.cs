using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EmployeeCrudApi.Models;

namespace EmployeeCrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Hardcoded list to simulate database
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Alice", Salary = 50000, Permanent = true },
            new Employee { Id = 2, Name = "Bob", Salary = 60000, Permanent = false }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return Ok(_employees);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee emp)
        {
            _employees.Add(emp);
            return Ok(emp);
        }

        [HttpPut("{id}")]
        public ActionResult<Employee> Put(int id, [FromBody] Employee emp)
        {
            // Requirement: Check if the id value is lesser than or equal to 0
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            // Requirement: Check if value is available in the hardcoded list
            var existingEmp = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmp == null)
            {
                // Requirement: throw BadRequest action result with the same message
                return BadRequest("Invalid employee id"); 
            }

            // Requirement: Use JSON data from input body and update the hardcoded list
            existingEmp.Name = emp.Name;
            existingEmp.Salary = emp.Salary;
            existingEmp.Permanent = emp.Permanent;
            
            // Requirement: Return filtered output
            return Ok(existingEmp);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingEmp = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmp != null)
            {
                _employees.Remove(existingEmp);
            }
            return Ok();
        }
    }
}
