using EmployeeManagement.models;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.BusinessLogic
{
	/**
	 * This calss is used for storing and retreiving Employees. there should no be any interaction of 
	 * the HTTP protocol in this class. It is completely independant.
	 * It implements the neccesary operations for maintaining Employees.
	 */
	public class MockEmployeeRepository
	{
		// Initialize a memory storage for our Employees. In real life we will use EFCore. 
		// We have to pay attention that in real life even such memeory implementation is not good since, several 
		// users can call same methods and we must protect it wiht locking / semaphors mechanism.
		// ususally when we use database, it take cares of this.
		private List<Employee> _employeeList = new List<Employee>{
			new Employee() { Id = 1, FirstName = "Mary", LastName = "Antuanet", Email = "mary@pragimtech.com", Department = "HR",  },
			new Employee() { Id = 2, FirstName = "John", LastName = "Dow", Email = "john@pragimtech.com", Department = "IT" },
			new Employee() { Id = 3, FirstName = "Sam", LastName = "Dow", Email = "sam@pragimtech.com", Department = "IT" },
		};

		public MockEmployeeRepository()
		{

		}

		public IEnumerable<Employee> GetAllEmployees()
		{
			return _employeeList;
		}

		public Employee GetEmployeeById(int id) {
			Employee employee = _employeeList.Find(x => x.Id == id);
			if (employee == null)
				throw new ArgumentException($"There is no Employee with Id == {id}");
			return employee;
		}

		// We retrun the Employee in the add medhod since it might be that some of the properties of the Employee object were changed
		// during the adding operation, for example if there is an ID, it is usually set while entering the object to the storage.
		public Employee AddEmployee(Employee employee) {

			employee.Id = GetEmployeeCount() + 1;
			_employeeList.Add(employee);
			return employee;
		}

		public Employee UpdateEmployee(int id, Employee updatedEmployeeData)
		{
			Employee employee = GetEmployeeById(id);
			if (employee == null)
			{
				throw new ArgumentException($"No Employee with Id = {id}");
			}
			else
			{
				employee.FirstName = updatedEmployeeData.FirstName != null ? updatedEmployeeData.FirstName : employee.FirstName;
				employee.LastName = updatedEmployeeData.LastName != null ? updatedEmployeeData.LastName : employee.LastName;
				employee.Department = updatedEmployeeData.Department != null ? updatedEmployeeData.Department : employee.Department;
				employee.Email = updatedEmployeeData.Email != null ? updatedEmployeeData.Email : employee.Email;
			}
			return employee;

		}

		public Employee UpdateEmployee(int id, string fitstName, string lastName, string email, string department)
		{
			Employee employee = GetEmployeeById(id);
			if (employee == null)
			{
				throw new ArgumentException($"No Employee with Id = {id}");
			}
			else {
				employee.FirstName = fitstName != null ? fitstName : employee.FirstName;
				employee.LastName = lastName != null ? lastName : employee.LastName;
				employee.Department = department != null ? department : employee.Department;
				employee.Email = email != null ? email : employee.Email;

			}
			return employee;
		}

		public Employee DeleteEmployee(int id)
		{
			Employee employee = GetEmployeeById(id);
			if (employee == null)
			{
				throw new ArgumentException($"No Employee with Id = {id} to delete");
			}
			else
			{
				bool success = _employeeList.Remove(employee);
				if (!success) {
					throw new Exception($"Some sort of storage error");
				}
			}

			return employee;
		}

		public int GetEmployeeCount() {
			return _employeeList.Count;
		}


	}
}
