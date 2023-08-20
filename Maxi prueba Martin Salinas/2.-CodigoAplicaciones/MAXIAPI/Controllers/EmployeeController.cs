using ENTITIES;
using MAXIAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MAXIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get()
        {
            return new Response { IsSuccess = true, Result = await _employee.GetEmployees() };
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] Employee employee)
        {
            var emp = await _employee.Add(employee);
            return emp != null ? new Response { IsSuccess = true, Message = "Employee saved successfully", Result = emp } : new Response { IsSuccess = false, Message = "Error trying to add employee", Result = emp };
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put([FromBody] Employee employee)
        {
            var emp = await _employee.Update(employee);
            return emp != null ? new Response { IsSuccess = true, Message = "Employee updated successfully", Result = employee } : new Response { IsSuccess = false, Message = "Error trying to update employee", Result = employee };
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var message = await _employee.Delete(id);
            return string.IsNullOrEmpty(message) ? new Response { IsSuccess = true, Message = "Employee deleted successfully", Result = id } : new Response { IsSuccess = false, Message = "Error trying to delete employee", Result = id };
        }
    }
}
