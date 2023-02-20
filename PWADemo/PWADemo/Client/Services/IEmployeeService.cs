using System;
using PWADemo.Shared;

namespace PWADemo.Client.Services
{
	public interface IEmployeeService
	{
		List<Employee> Employees { get; set; }

        Task GetEmployees();
		Task<Employee> GetSingleEmployee(int id);
		Task CreateEmployee(Employee employee);
		Task UpdateEmployee(Employee employee);
		Task DeleteEmployee(int id);
    }
}

