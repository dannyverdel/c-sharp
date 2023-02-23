using System;
using ApiDemo.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiDemo.Services
{
    public class EmployeeService : IEmployeeService
    {
        private List<EmployeeModel> _employees = new EmployeeModelFaker().Generate(Random.Shared.Next(500, 1001));

        private readonly IMemoryCache _memory_cache;

        public EmployeeService(IMemoryCache memory_cache) => _memory_cache = memory_cache;

        public ResponseModel<List<EmployeeModel>> GetEmployees() {
            List<EmployeeModel>? employees = _memory_cache.Get<List<EmployeeModel>>("employees");

            if ( employees is null ) {
                Task.Delay(3000).Wait();
                employees = _employees;
                _memory_cache.Set<List<EmployeeModel>>("employees", employees, TimeSpan.FromMinutes(1));
            }
            return new ResponseModel<List<EmployeeModel>>(HttpStatusCode.OK, employees);
        }

        public ResponseModel<EmployeeModel> GetSingleEmployee(int id) {
            EmployeeModel? employee = GetEmployees().GetData()?.Where(x => x.Id == id).FirstOrDefault();
            if ( employee is null )
                return new ResponseModel<EmployeeModel>(HttpStatusCode.NotFound, $"No employee found with id {id}");

            return new ResponseModel<EmployeeModel>(HttpStatusCode.OK, employee);
        }

        public ResponseModel<EmployeeModel> CreateEmployee(EmployeeModel employee) {
            int? id = GetEmployees().GetData()?.OrderByDescending(x => x.Id).First().Id;

            if ( id is null || id < 0 )
                id = 1;
            else
                id++;

            employee.Id = ( int ) id;
            _employees.Add(employee);
            return new ResponseModel<EmployeeModel>(HttpStatusCode.OK, employee);
        }

        public ResponseModel<EmployeeModel> UpdateEmployee(int id, EmployeeModel employee) {
            employee.Id = id;
            int? index = GetEmployees().GetData()?.FindIndex(x => x.Id == employee.Id);
            if ( index < 0 || index is null )
                return new ResponseModel<EmployeeModel>(HttpStatusCode.NotFound, $"No employee found with id {id}");

            _employees[( int ) index] = employee;

            return new ResponseModel<EmployeeModel>(HttpStatusCode.OK, employee);
        }

        public ResponseModel<EmployeeModel> DeleteEmployee(int id) {
            EmployeeModel? employee = GetEmployees().GetData()?.Where(x => x.Id == id).FirstOrDefault();
            if ( employee is null )
                return new ResponseModel<EmployeeModel>(HttpStatusCode.NotFound, $"No employee found with id {id}");

            _employees.Remove(employee);
            return new ResponseModel<EmployeeModel>(HttpStatusCode.OK, employee);
        }
    }
}

