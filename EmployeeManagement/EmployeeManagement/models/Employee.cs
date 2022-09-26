namespace EmployeeManagement.models
{
	/*
	 * Employee is the item we want to store in the database. It is recommended to store such objects in a record, insted of class.
	 * A Record is a very good and elegant way of defining a data structure that has a constructor that we can create, so it
	 * can simply represent a data transfer object, DTO, object that can be use for storing some data and transferring between 
	 * services.
	 */
	public record Employee
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Department { get; set; }
	}
}
