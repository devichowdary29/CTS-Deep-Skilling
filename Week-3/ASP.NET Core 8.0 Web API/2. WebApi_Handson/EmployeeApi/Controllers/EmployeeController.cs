using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeApi.Controllers
{
    // Lab 3: Modify the Controller name in the Route attribute to 'Emp'
    [Route("api/Emp")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Lab 2 & 3: Usage of ProducesResponseType and Name attribute
        [HttpGet(Name = "GetEmployees")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
        public IActionResult Get()
        {
            // Returns a list of employees for testing in Postman
            return Ok(new string[] { "Alice", "Bob", "Charlie" });
        }

        // Lab 3: Explain usage of ActionName to have more than 1 method with the same Action verb
        [HttpGet("details/{id}")]
        [ActionName("GetEmployeeDetails")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get(int id)
        {
            return Ok($"Employee Detail for {id}");
        }
    }
}
