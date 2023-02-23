using ApiDemo.Models;

namespace ApiDemo.Services
{
				public interface IEmployeeService
				{
								ResponseModel<EmployeeModel> CreateEmployee(EmployeeModel employee);
								ResponseModel<EmployeeModel> DeleteEmployee(int id);
								ResponseModel<List<EmployeeModel>> GetEmployees();
								ResponseModel<EmployeeModel> GetSingleEmployee(int id);
								ResponseModel<EmployeeModel> UpdateEmployee(int id, EmployeeModel employee);
				}
}