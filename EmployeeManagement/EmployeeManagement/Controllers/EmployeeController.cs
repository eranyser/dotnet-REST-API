using EmployeeManagement.BusinessLogic;
using EmployeeManagement.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		// Our EmployeeController needs access to the EmployeeRepository. we don't want to couple this 
		// controller with specific implementation, (that is why we won't do new here to the MockEmployeeRepository).
		// instead we will use depandancy injection.
		// We will create an interface for the EmployeeRepository, that serves as a contruct and we will use it here.
		// We will inject it in the startup.cs class and pass it in the constructor of this controller

		private readonly IEmployeeRepository _employeeRepository;

		// The constructor asks for IEmployeeRepository and get the concrete implementation that was register in the startup.cs file.
		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository= employeeRepository;
		}

		// Since we return here IActionResult, ASP.NET has a problem. It doesn't know what it is really returns. It doesn't know
		// it returns a collection of Employees. For that reason we add the ProducesResponceType attribute. 
		// https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0#iactionresult-type
		// in this attribute we states the StatusCodes we return and the object type we return. Actually we tell the ASP.NET:
		// if there is status code 200, it will return a collection of Employees.
		// 
		[HttpGet]
		[Route("all")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
		public IActionResult GetAllEmployees() {
			return Ok(_employeeRepository.GetAllEmployees());
		}

		// We want to give this route a technical name, so we will be able to use this method when we add new Employee.
		// To specify a name we can also use: 
		// [HttpGet("{id:int}", Name = nameof(GetEmployeeById))]
		[HttpGet("{id:int}", Name = "GetSpecificEmployee")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetEmployeeById(int id)
		{
			try
			{
				return Ok(_employeeRepository.GetEmployeeById(id));
			}
			catch (Exception ex)
			{
				// return BadRequest(ex.Message);
				return NotFound(ex.Message);
			}
		}


		// For the Add method, we have to state from where do we get the newEmployee - [FromBody]. We are getting the newEmployee
		// in a JSON format from the HTTP body.
		// instead of return status Ok, we will return here status Created, that request a URI for the newly created item.
		// We link here the result of the Add method with the GetEmployeeById method through the technical name we created.
		// Read about it here: https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
		[HttpPost("add")]
		public IActionResult AddEmployee([FromBody] Employee newEmployee)
		{
			Employee employee = _employeeRepository.AddEmployee(newEmployee);
			// return Created($"https://localhost:5001/api/employee/{employee.Id}", employee);
			return CreatedAtRoute("GetSpecificEmployee", new { Id = employee.Id }, employee);
			// return CreatedAtRoute(nameof(GetEmployeeById), new { Id = employee.Id }, employee);
		}

		[HttpPut]
		[Route("update/{id}")]
		public IActionResult UpdateEmployee(int id, [FromBody] Employee e)
		{
			try
			{
				return Ok(_employeeRepository.UpdateEmployee(id, e));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("updateq/{id}")]
		public IActionResult UpdateEmployeeByQuery(int id, [FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string email, [FromQuery] string department)
		{
			try
			{
				return Ok(_employeeRepository.UpdateEmployee(id, firstName, lastName, email, department));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("delete/{Id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteEmployee(int Id)
		{
			try
			{
				Employee employee = _employeeRepository.DeleteEmployee(Id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
