using System;
using PWADemo.Shared;
using Microsoft.AspNetCore.Mvc;
namespace PWADemo.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		public static List<Employee> employees = new EmployeeFaker().Generate(100);

		[HttpGet]
		public ActionResult<List<Employee>> GetEmployees()
		{
			return Ok(employees);
		}

		[HttpGet("{id}")]
		public ActionResult<Employee> GetSingleEmployee(int id) => employees.Where(x => x.Id == id).First();

		[HttpPost]
		public ActionResult<List<Employee>> CreateEmployee(Employee employee)
		{
            employees.Add(employee);
			return Ok(employees);
        }

        [HttpPut("{id}")]
		public ActionResult<List<Employee>> UpdateEmployee(Employee employee, int id)
		{
			int index = employees.FindIndex(x => x.Id == id);
			if (index < 0) return NotFound("No employee here. :/");
            employees[index] = employee;
			return Ok(employees);
		}

		[HttpDelete("{id}")]
		public ActionResult<List<Employee>> DeleteEmployee(int id)
		{
			Employee? employee = employees.Where(x => x.Id == id).FirstOrDefault();
			if (employee is null) return NotFound("No employee here. :/");
            employees.Remove(employee);
			return Ok(employees);
		}
	}
}

