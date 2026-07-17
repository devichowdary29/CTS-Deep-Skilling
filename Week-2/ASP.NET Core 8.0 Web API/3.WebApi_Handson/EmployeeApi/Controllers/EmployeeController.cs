using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EmployeeApi.Models;
using EmployeeApi.Filters;
using System;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthFilter] // Lab 3: Intercept and check Authorization header
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {
            // Constructor: Creates few records virtually by providing GetStandardEmployeeList
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1, Name = "Alice", Salary = 50000, Permanent = true, DateOfBirth = new DateTime(1990, 1, 1),
                    Department = new Department { Id = 1, Name = "IT" },
                    Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" } }
                }
            };
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Employee>))]
        [ProducesResponseType(500)] // Lab 3: Add ProducesResponseType for 500
        [TypeFilter(typeof(CustomExceptionFilter))] // Lab 3: Apply Exception Filter
        public ActionResult<List<Employee>> Get()
        {
            // Trigger exception context if "throw" query string is present
            if (Request.Query.ContainsKey("throw"))
            {
                throw new Exception("Intentional Exception to trigger CustomExceptionFilter.");
            }
            
            return GetStandardEmployeeList();
        }

        // Lab 3: HTTPPost and HTTPPut using FromBody
        [HttpPost]
        public IActionResult Post([FromBody] Employee emp)
        {
            // Simulate creation logic
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Employee emp)
        {
            // Simulate update logic
            return Ok();
        }
    }
}
