using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using PWADemo.Shared;

namespace PWADemo.Client.Services
{
	public class EmployeeService : IEmployeeService
	{
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation_manager;

        public List<Employee> Employees { get; set; } = new List<Employee>();

		public EmployeeService(HttpClient http, NavigationManager navigation_manager)
		{
            _http = http;
            _navigation_manager = navigation_manager;
		}

        public async Task CreateEmployee(Employee employee)
        {
            int id = Employees.Count == 0 ? 1 : Employees.OrderByDescending(x => x.Id).ToList().First().Id + 1;
            employee.Id = id;
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/employee", employee);
            await SetEmployees(response);
        }

        public async Task DeleteEmployee(int id)
        {
            HttpResponseMessage response = await _http.DeleteAsync($"api/employee/{id}");
            await SetEmployees(response);
        }

        public async Task GetEmployees()
        {
            List<Employee>? employees = await _http.GetFromJsonAsync<List<Employee>>("api/employee");
            if (employees is not null) Employees = employees;
        }

        public async Task<Employee> GetSingleEmployee(int id)
        {
            Employee? employee = await _http.GetFromJsonAsync<Employee>($"api/employee/{id}");
            if (employee is not null) return employee;
            throw new Exception($"No employee found with the id {id}");
        }

        public async Task UpdateEmployee(Employee employee)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/employee/{employee.Id}", employee);
            await SetEmployees(response);
        }

        private async Task SetEmployees(HttpResponseMessage response)
        {
            List<Employee>? result = await response.Content.ReadFromJsonAsync<List<Employee>>();
            if (result is not null) Employees = result;
            _navigation_manager.NavigateTo("employees");
        }
    }
}

