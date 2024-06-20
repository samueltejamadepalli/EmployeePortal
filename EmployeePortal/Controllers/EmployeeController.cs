using EmployeePortal.Data;
using EmployeePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(AppDbContext dbContext) : ControllerBase
    {
        private readonly AppDbContext dbContext = dbContext;

        [HttpGet]

        public IActionResult GetEmployees()
        {

            var employees = dbContext.Employees.ToList();

            return Ok(employees);
        }
        [HttpGet]
        [Route("id:int")]
        public IActionResult GetEmployee(int id)
        {
            try
            {
               
                var employee = dbContext.Employees.SingleOrDefault(x => x.Id == id);
                return Ok(employee);
            }
            catch (Exception)
            {

                return BadRequest($"Employee Not Found With id{id}");
            }
        }

        [HttpPost]

        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employee = new Employee();
            {
                employee.FirstName = addEmployeeDto.FirstName;
                employee.LastName = addEmployeeDto.LastName;
                employee.Email = addEmployeeDto.Email;
                employee.Phone = addEmployeeDto.Phone;
                employee.Address = addEmployeeDto.Address;

            }
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }
        [HttpPut]
        [Route("int:id")]

        public IActionResult EditEmployee(int id, [FromBody] EditEmployeeDto editEmployeeDto)
        {
            var employee = dbContext.Employees.SingleOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            {
                employee.FirstName = editEmployeeDto.FirstName;
                employee.LastName = editEmployeeDto.LastName;
                employee.Email = editEmployeeDto.Email;
                employee.Phone = editEmployeeDto.Phone;
                employee.Address = editEmployeeDto.Address;

            }

            dbContext.SaveChanges();

            var employeedto = new EditEmployeeDto
            {
                FirstName = editEmployeeDto.FirstName,
                LastName = editEmployeeDto.LastName,
                Email = editEmployeeDto.Email,
                Phone = editEmployeeDto.Phone,
                Address = editEmployeeDto.Address,

            };
            
            return Ok(employeedto);


        }

        [HttpDelete]
        [Route("int:id")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var employee = dbContext.Employees.SingleOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    return NotFound();
                }
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();

                return Ok("Deleted Sucessfully");
            }
            catch (Exception)
            {
                return NotFound("Invaild Id");
            }
        }


    }
}
