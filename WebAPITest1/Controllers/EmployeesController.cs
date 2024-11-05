using Microsoft.AspNetCore.Mvc;
using WebAPITest1.Data.Interfaces;
using WebAPITest1.Models;
using WebAPITest1.Models.Entities;

namespace WebAPITest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                return Ok(repository.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            try
            {
                var employee = repository.GetOne(id);
                if (employee is null)
                {
                    return NotFound("Employee not found");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employee = new Employee
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            return Ok(repository.Post(employee));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {

            var employee = new Employee
            {
                Id = id,
                Name = updateEmployeeDto.Name,
                Email = updateEmployeeDto.Email,
                Phone = updateEmployeeDto.Phone,
                Salary = updateEmployeeDto.Salary
            };

            var updatedEmployee = repository.Put(employee);

            if (updatedEmployee is null)
            {
                return NotFound("Employee not found");
            }

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = repository.Delete(id);
            if (!employee)
            {
                return NotFound("Employee not found");
            }
            return Ok();
        }

    }
}
