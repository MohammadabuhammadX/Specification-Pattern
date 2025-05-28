namespace API.Dtos
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public bool Status { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }

}
