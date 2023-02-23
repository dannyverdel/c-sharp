using System;
using ApiDemo.Models;
using ApiDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiDemo.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employee_service;

    public EmployeeController(IEmployeeService employee_service) => _employee_service = employee_service;

    [HttpGet]
    public IActionResult? GetEmployees() => GetActionResult<List<EmployeeModel>>(_employee_service.GetEmployees());

    [HttpGet("{id}")]
    public IActionResult? GetEmployee(int id) => GetActionResult<EmployeeModel>(_employee_service.GetSingleEmployee(id));

    [HttpPost]
    public IActionResult? CreateEmployee(EmployeeModel employee) => GetActionResult<EmployeeModel>(_employee_service.CreateEmployee(employee));

    [HttpPut("{id}")]
    public IActionResult? UpdateEmployee(int id, EmployeeModel employee) => GetActionResult<EmployeeModel>(_employee_service.UpdateEmployee(id, employee));

    [HttpDelete("{id}")]
    public IActionResult? DeleteEmployee(int id) => GetActionResult<EmployeeModel>(_employee_service.DeleteEmployee(id));

    private IActionResult? GetActionResult<T>(ResponseModel<T> response) {
        IActionResult? result;

        switch ( response.StatusCode ) {
            case HttpStatusCode.OK:
                result = Ok(response.GetData());
                break;
            case HttpStatusCode.Unauthorized:
                result = Unauthorized(response);
                break;
            case HttpStatusCode.BadRequest:
                result = BadRequest(response);
                break;
            case HttpStatusCode.NotFound:
                result = NotFound(response);
                break;
            default:
                result = null;
                break;
        }

        return result;
    }
}

