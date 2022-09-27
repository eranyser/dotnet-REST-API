using EmployeeManagement.models;
using System.Collections.Generic;

namespace EmployeeManagement.BusinessLogic
{
	// In order to use depandacy injection we create interface for our EmployeeRepository
	public interface IEmployeeRepository
	{
		IEnumerable<Employee> GetAllEmployees();
		Employee GetEmployeeById(int id);
		Employee AddEmployee(Employee employee);
		Employee UpdateEmployee(int id, Employee updatedEmployeeData);
		Employee UpdateEmployee(int id, string fitstName, string lastName, string email, string department);
		Employee DeleteEmployee(int id);
		int GetEmployeeCount();
	}
}
