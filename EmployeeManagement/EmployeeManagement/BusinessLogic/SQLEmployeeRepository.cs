using EmployeeManagement.models;
using System.Collections.Generic;

namespace EmployeeManagement.BusinessLogic
{
	public class SQLEmployeeRepository : IEmployeeRepository
	{
		private readonly AppDbContext _context;

		public SQLEmployeeRepository(AppDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Employee> GetAllEmployees()
		{
			return _context.Employees;
		}

		public Employee GetEmployeeById(int id)
		{
			return _context.Employees.Find(id);
		}

		public Employee AddEmployee(Employee newEmployee)
		{
			_context.Employees.Add(newEmployee);
			_context.SaveChanges(); // this method must be called in order to save teh new Employee under the Database.
			return newEmployee;

		}

		public Employee UpdateEmployee(int id, Employee updatedEmployeeData)
		{
			var employee = _context.Employees.Attach(updatedEmployeeData);
			employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_context.SaveChanges();
			return updatedEmployeeData;
		}

		public Employee UpdateEmployee(int id, string fitstName, string lastName, string email, string department)
		{
			throw new System.NotImplementedException();
		}

		public Employee DeleteEmployee(int id)
		{
			Employee employee = _context.Employees.Find(id);
			if (employee != null)
			{
				_context.Employees.Remove(employee);
				_context.SaveChanges();
			}
			return employee;

		}

		public int GetEmployeeCount()
		{
			throw new System.NotImplementedException();
		}

	}
}
